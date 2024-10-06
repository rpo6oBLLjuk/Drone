using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IUIContentWidget : IUITabWidget
{
    [SerializeField] protected List<GameObject> contentContainers = new();
    private Dictionary<GameObject, List<GameObject>> contents = new();

    public int CurrentContentIndex
    {
        get => Mathf.Clamp(currentContentIndex, 0, contents[contentContainers[currentTabIndex]].Count - 1);
        set
        {
            int lastIndex = Mathf.Clamp(currentContentIndex, 0, contents[contentContainers[currentTabIndex]].Count - 1);
            currentContentIndex = Mathf.Clamp(value, 0, contents[contentContainers[currentTabIndex]].Count - 1);
            SetContentFocus(lastIndex, currentContentIndex);
        }
    }
    [SerializeField, ReadOnly] protected int currentContentIndex;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] protected float changeContentIndexDuration = 0.5f;
    [SerializeField, ProgressBar(0, nameof(changeTabIndexDuration), isReadOnly = true)] protected float currentChangeContentIndexTime = 0.5f;

    [SerializeField] protected Color contentActiveColor;
    [SerializeField] protected Color contentInactiveColor;

    public virtual void Start()
    {
        foreach (GameObject container in contentContainers)
        {
            List<GameObject> list = container
                .GetComponentsInChildren<Transform>(true)
                .Where(transform => container.transform != transform)
                .Select(transform => transform.gameObject)
                .ToList();

            contents.Add(container, list);

            Debug.Log($"Content: {container.name}, count: {contents[container].Count}");
        }
    }

    public override void ShowWidget()
    {
        base.ShowWidget();

        CurrentContentIndex = 0;

        currentChangeContentIndexTime = changeContentIndexDuration;
    }

    public override void ActivateFocused()
    {
        contentContainers[currentTabIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    public void ContentMovement(float axis)
    {
        CalculateChangeContentIndexTime(axis);
    }

    public void CalculateChangeContentIndexTime(float inputAxis)
    {
        currentChangeContentIndexTime += Time.unscaledDeltaTime;

        if (currentChangeContentIndexTime > changeContentIndexDuration)
        {
            currentChangeContentIndexTime = 0;
            CurrentContentIndex += (int)Mathf.Sign(inputAxis);
        }
    }

    protected override void SetTabFocus(int lastIndex, int newIndex)
    {
        base.SetTabFocus(lastIndex, newIndex);

        ChangeContentContainer(lastIndex, newIndex);
    }


    protected void ChangeContentContainer(int lastIndex, int newIndex)
    {
        contentContainers[lastIndex].SetActive(false);
        contentContainers[newIndex].SetActive(true);

        CurrentContentIndex = 0;
    }

    protected virtual void SetContentFocus(int lastIndex, int newIndex)
    {
        contents[contentContainers[newIndex]][lastIndex].GetComponentInChildren<UnityEngine.UI.Image>().color = contentInactiveColor;
        contents[contentContainers[newIndex]][newIndex].GetComponentInChildren<UnityEngine.UI.Image>().color = contentActiveColor;

        EventSystem.current.SetSelectedGameObject(tabs[newIndex]);

        //RectTransform scrollView = scrollRect.viewport;
        //if (!RectTransformUtility.RectangleContainsScreenPoint(scrollView, contentContainers[currentContentIndex].transform.position))
        //{
        //    scrollRect.verticalNormalizedPosition = 1 - (float)currentContentIndex / (contentContainers.Count - 1);
        //}
    }
}
