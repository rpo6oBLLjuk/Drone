using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class UIOptions : IUIWidget
{
    public CanvasGroup Widget
    {
        get => _optionsContainer;
        set => _optionsContainer = value;
    }
    [SerializeField] private CanvasGroup _optionsContainer;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollSpeed;

    [SerializeField] private List<OptionsObjectContainer> settings_menu;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    public int ActiveMenuIndex
    {
        get => activeMenuIndex;
        set => activeMenuIndex = Mathf.Clamp(value, 0, settings_menu.Count - 1);
    }
    [SerializeField] private int activeMenuIndex;


    public void Start()
    {
        for (int i = 1; i < settings_menu.Count; i++)
        {
            EnableMenu(i, 0);
        }
    }

    public void ChangeActiveMenu(int axis)
    {
        int currentIndex = ActiveMenuIndex;
        ActiveMenuIndex += axis;

        EnableMenu(currentIndex, ActiveMenuIndex);
    }

    private void EnableMenu(int disabledMenu, int enabledMenu)
    {
        settings_menu[disabledMenu].menuObj.GetComponentInChildren<UnityEngine.UI.Image>().color = inactiveColor;
        settings_menu[disabledMenu].settingsContainer.SetActive(false);

        settings_menu[enabledMenu].menuObj.GetComponentInChildren<UnityEngine.UI.Image>().color = activeColor;
        settings_menu[enabledMenu].settingsContainer.SetActive(true);
    }


    public void MoveScrollRect(Vector2 value)
    {
        //float contentHeight = scrollRect.content.sizeDelta.y;
        //float contentShift = scrollSpeed * Time.deltaTime;
        //scrollRect.verticalNormalizedPosition += contentShift / contentHeight;
        scrollRect.verticalNormalizedPosition += value.y * scrollRect.content.sizeDelta.y * scrollSpeed;
    }


    [Serializable]
    public class OptionsObjectContainer
    {
        public GameObject menuObj;
        public GameObject settingsContainer;
    }
}
