using CustomInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PopupWidget : IUIWidget
{
    [HorizontalLine("Popup fields")]
    [SerializeField] private ReorderableDictionary<PopupType, GameObject> popups;

    private Action currentAction;
    private PopupType currentPopupType;

    public void ShowWidget(string popupText, Action action, PopupType type)
    {
        base.ShowWidget();

        currentPopupType = type;
        currentAction = action;

        GameObject popup = popups[type];
        popup.SetActive(true);

        foreach (TextMeshProUGUI tmpro in popup.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (tmpro.gameObject.name.Contains("opup"))
            {
                tmpro.text = popupText;
            }
        }

        uiService.uiWidgetsController.widgetSequence.Add((new IUIWidget[] { this }, type is PopupType.okCancel));
    }

    public override void Apply()
    {
        currentAction?.Invoke();

        InputAction.CallbackContext context = new();

        uiService.uiWidgetsController.HideActiveWidget(context);
    }

    public override void HideWidget()
    {
        base.HideWidget();

        currentAction = null;

        popups[currentPopupType].SetActive(false);
    }
}

public enum PopupType
{
    ok,
    okCancel
}
