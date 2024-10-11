using CustomInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IUITabWidget : IUIWidget
{
    [HorizontalLine("Tabs fields")]
    [SerializeField] protected List<GameObject> tabs = new();
    public int CurrentTabIndex
    {
        get => currentTabIndex;
        set
        {
            int lastIndex = currentTabIndex;
            currentTabIndex = Mathf.Clamp(value, 0, tabs.Count - 1);
            SetTabFocus(lastIndex, currentTabIndex);
        }
    }
    [SerializeField, ReadOnly] protected int currentTabIndex;

    [SerializeField] protected float changeTabIndexDuration = 0.5f;
    [SerializeField, ProgressBar(0, nameof(changeTabIndexDuration), isReadOnly = true)] protected float currentChangeTabIndexTime = 0.5f;

    [SerializeField] protected Color tabActiveColor;
    [SerializeField] protected Color tabInactiveColor;


    public override void ShowWidget()
    {
        base.ShowWidget();

        CurrentTabIndex = 0;

        currentChangeTabIndexTime = 0;
    }

    public void TabMovement(float inputAxis)
    {
        CalculateChangeTabIndexTime(inputAxis, false);
    }

    public void CalculateChangeTabIndexTime(float inputAxis, bool forceChange)
    {
        currentChangeTabIndexTime += Time.unscaledDeltaTime;

        if (currentChangeTabIndexTime > changeTabIndexDuration || forceChange)
        {
            currentChangeTabIndexTime = 0;
            ChangeTabIndex(inputAxis);
        }
    }

    public virtual void ActivateFocused()
    {
        tabs[currentTabIndex].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangeTabIndex(float value)
    {
        CurrentTabIndex += (int)Mathf.Sign(value);
        Debug.Log("Tab changed");
    }

    protected virtual void SetTabFocus(int lastIndex, int newIndex)
    {
        tabs[lastIndex].GetComponentInChildren<Image>().color = tabInactiveColor;
        tabs[newIndex].GetComponentInChildren<Image>().color = tabActiveColor;

    }
}
