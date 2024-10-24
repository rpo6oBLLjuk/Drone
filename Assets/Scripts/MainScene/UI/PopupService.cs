using CustomInspector;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupService : MonoBehaviour
{
    public static PopupService instance;
    [SerializeField] private CanvasGroup widget;
    [SerializeField] private ReorderableDictionary<PopupType, GameObject> popups;

    [SerializeField] private float animDuration;
    [SerializeField] private Color positivePopupColor;
    [SerializeField] private Color negativePopupColor;

    private void Awake()
    {
        instance = this;
    }

    public void ShowPopup(string text, PopupType popupType, bool isPositive)
    {
        GameObject popup = Instantiate(popups[popupType], widget.transform);

        widget.DOFade(1, 0.25f)
            .OnStart(() =>
            {
                widget.blocksRaycasts = true;
                widget.interactable = true;
            });

        SetText(text, popup);
        SetOutline(isPositive, popup);
        SetButtonsFunction(popup);
    }

    private void HidePopup()
    {
        widget.DOFade(0, 0.25f)
            .OnStart(() =>
            {
                widget.blocksRaycasts = false;
                widget.interactable = false;
                widget.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
            });
    }


    //����� �������� ������ ������ �� Assets/Scripts/DroneScene_base/UI/system/UIWidgets/PopupWidget, ����� ������� ������ ���������� � ��������� (������������ �� ��������, �.�. PopupWidget ������� �� IUIWidget
    private void SetText(string text, GameObject popup)
    {
        foreach (TextMeshProUGUI tmpro in popup.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (tmpro.gameObject.name.ToLower().Contains("popup"))
            {
                tmpro.text = text;
            }
        }
    }

    private void SetOutline(bool isPositive, GameObject popup)
    {
        popup.GetComponentInChildren<Outline>().effectColor = isPositive ? positivePopupColor : negativePopupColor;
    }

    private void SetButtonsFunction(GameObject popup)
    {
        foreach (var tmpro in popup.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (tmpro.gameObject.name.ToLower().Contains("ok"))
            {
                tmpro.onClick.AddListener(OkButtonClick);
            }

            if (tmpro.gameObject.name.ToLower().Contains("cancel"))
            {
                tmpro.onClick.AddListener(CancelButtonClick);
            }
        }
    }

    public void OkButtonClick()
    {
        HidePopup();
    }

    public void CancelButtonClick()
    {
        HidePopup();
    }
}
