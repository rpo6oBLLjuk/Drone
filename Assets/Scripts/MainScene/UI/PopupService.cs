using CustomInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class PopupService : MonoBehaviour
{
    [SerializeField] private CanvasGroup widget;
    [SerializeField] private ReorderableDictionary<PopupType, GameObject> popups;

    [SerializeField] private float animDuration;
    [SerializeField] private Color positivePopupColor;
    [SerializeField] private Color negativePopupColor;

    public void ShowPopup(string text, PopupType popupType, bool isPositive)
    {
        GameObject popup = Instantiate(popups[popupType], widget.transform);

        widget.DOFade(1, 0.25f)
            .OnStart(() =>
            {
                widget.blocksRaycasts = true;
                widget.interactable = true;
            })
            .OnComplete(()=>
            {
                if(isPositive)
                    StateController.AuthComplete?.Invoke();
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


    //Метод является копией метода из Assets/Scripts/DroneScene_base/UI/system/UIWidgets/PopupWidget, стоит вынести данный функционал в интерфейс (наследование не подходит, т.к. PopupWidget зависим от IUIWidget
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
