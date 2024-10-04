using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIOptionsController : IUIWidget
{
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
            EnableMenu(0, i);
        }
    }


    public void ChangeActiveMenu(float axis)
    {
        int correntAxis = (int)Mathf.Sign(axis);

        int currentIndex = ActiveMenuIndex;
        ActiveMenuIndex += correntAxis;

        EnableMenu(currentIndex, ActiveMenuIndex);
    }

    private void EnableMenu(int enabledMenu, int disabledMenu)
    {
        settings_menu[disabledMenu].settingTab.GetComponentInChildren<UnityEngine.UI.Image>().color = inactiveColor;
        settings_menu[disabledMenu].settingsContainer.SetActive(false);

        settings_menu[enabledMenu].settingTab.GetComponentInChildren<UnityEngine.UI.Image>().color = activeColor;
        settings_menu[enabledMenu].settingsContainer.SetActive(true);
    }


    public void MoveScrollRect(float value)
    {
        
        //scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition + value * scrollRect.content.sizeDelta.y * scrollSpeed);//, -1, 1);
    }


    [Serializable]
    public class OptionsObjectContainer
    {
        public GameObject settingTab;
        public GameObject settingsContainer;
    }
}
