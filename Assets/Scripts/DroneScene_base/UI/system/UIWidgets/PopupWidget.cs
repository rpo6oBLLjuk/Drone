using CustomInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[Serializable]
public class PopupWidget : IUIWidget
{
    [Inject] UIService uiService;

    [HorizontalLine("Popup fields")]
    [SerializeField] private ReorderableDictionary<PopupType, GameObject> popups;

    private Action currentAction;
    private PopupType currentPopupType;

    private GameObject currentPopup;

    public override void Start()
    {
        base.Start();

        hideCompleted += HideCompleted;
    }

    public override void Dispose()
    {
        base.Dispose();

        hideCompleted -= HideCompleted;
    }

    public void ShowWidget(string popupText, Action action, PopupType type)
    {
        base.ShowWidget();

        currentPopupType = type;
        currentAction = action;

        currentPopup = UnityEngine.Object.Instantiate(popups[type], Widget.transform);
        currentPopup.SetActive(true);

        foreach (TextMeshProUGUI tmpro in currentPopup.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (tmpro.gameObject.name.ToLower().Contains("popup"))
            {
                tmpro.text = popupText;
            }
        }

        uiService.uiWidgetsController.widgetSequence.Add((this, type is PopupType.okCancel));
    }

    public override void Apply()
    {
        currentAction?.Invoke();

        InputAction.CallbackContext context = new();

        uiService.uiWidgetsController.Close(context);
    }

    public override void HideWidget()
    {
        base.HideWidget();

        currentAction = null;
    }

    private void HideCompleted()
    {
        UnityEngine.Object.Destroy(currentPopup);
    }
}


