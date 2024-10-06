using CustomInspector;
using ModestTree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IUIContentWidget : IUITabWidget
{
    [SerializeField] protected List<GameObject> contentContainers = new();
    private List<GameObject> currentContent = new();

    public int CurrentContentIndex
    {
        get => Mathf.Clamp(currentContentIndex, 0, currentContent.Count - 1);
        set
        {
            int lastIndex = Mathf.Clamp(currentContentIndex, 0, currentContent.Count - 1);
            currentContentIndex = Mathf.Clamp(value, 0, currentContent.Count - 1);
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
        if(contentContainers.Count != tabs.Count)
        {
            Debug.LogError("Ошибка IUIContent виджета: количество tab-элементов не соответствует количеству content-элементов");
            Debug.Break();
        }

        SetCurrentContent();

        //Ранее сохранялись все списки контента. Теперь сохраняется лишь 1 актуальный список, однако код оставлен в качестве "запасного"
        //
        //foreach (GameObject container in contentContainers)
        //{
        //    List<GameObject> list = container
        //        .GetComponentsInChildren<Transform>(true)
        //        .Where(transform => container.transform != transform)
        //        .Select(transform => transform.gameObject)
        //        .ToList();

        //    currentContents.Add(container, list);

        //    Debug.Log($"Content: {container.name}, count: {currentContents[container].Count}");
        //}
    }

    public override void ActivateFocused()
    {
        contentContainers[currentTabIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    public void ContentMovement(float axis)
    {
        CalculateChangeContentIndexTime(axis, false);
    }

    public void CalculateChangeContentIndexTime(float inputAxis, bool forceChange)
    {
        currentChangeContentIndexTime += Time.unscaledDeltaTime;

        if (currentChangeContentIndexTime > changeContentIndexDuration || forceChange)
        {
            currentChangeContentIndexTime = 0;
            CurrentContentIndex += (int)Mathf.Sign(inputAxis);
        }
    }

    protected override void SetTabFocus(int lastIndex, int newIndex)
    {
        base.SetTabFocus(lastIndex, newIndex);

        currentContent[lastIndex].GetComponentInChildren<UnityEngine.UI.Image>(true).color = contentInactiveColor;

        SetCurrentContent();

        currentChangeContentIndexTime = 0;

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
        currentContent[lastIndex].GetComponentInChildren<UnityEngine.UI.Image>(true).color = contentInactiveColor;
        currentContent[newIndex].GetComponentInChildren<UnityEngine.UI.Image>().color = contentActiveColor;

        //Аналогично описанному выше
        //currentContents[contentContainers[newIndex]][lastIndex].GetComponentInChildren<UnityEngine.UI.Image>().color = contentInactiveColor;
        //currentContents[contentContainers[newIndex]][newIndex].GetComponentInChildren<UnityEngine.UI.Image>().color = contentActiveColor;

        EventSystem.current.SetSelectedGameObject(tabs[newIndex]);

        //Код прокрутки ScrollRect за элементом, будет введён позже
        //RectTransform scrollView = scrollRect.viewport;
        //if (!RectTransformUtility.RectangleContainsScreenPoint(scrollView, contentContainers[currentContentIndex].transform.position))
        //{
        //    scrollRect.verticalNormalizedPosition = 1 - (float)currentContentIndex / (contentContainers.Count - 1);
        //}
    }

    private void SetCurrentContent()
    {
        currentContent = contentContainers[currentTabIndex].GetComponentsInChildren<Transform>(true)
                .Where(transform => contentContainers[currentTabIndex].transform != transform)
                .Select(transform => transform.gameObject)
                .ToList();
    }
}
