using CustomInspector;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[Serializable]
public abstract class IUIWidget
{
    [Inject] protected UIService uiService;

    public CanvasGroup Widget
    {
        get => _widget;
        set => _widget = value;
    }
    [SerializeField] private CanvasGroup _widget;

    [SerializeField] protected float animDuration;

    [SerializeField, ReadOnly] public bool canBeShow = true;

    public virtual void Start()
    {

    }

    public virtual void Dispose()
    {

    }

    public virtual void ShowWidget()
    {
        if (canBeShow)
        {
            Widget.interactable = true;
            Widget.DOFade(1, animDuration)
                .SetUpdate(true);
        }
    }
    public virtual void HideWidget()
    {
        Widget.interactable = false;
        Widget.DOFade(0, animDuration)
            .SetUpdate(true);
    }

    public virtual void Apply()
    {

    }

    public void InjectDependencies(DiContainer container)
    {
        container.Inject(this);

        Widget.alpha = 0; //Не лучшее решение, но виджет априори должен быть отключён при создании, и дёргать лишний метод я смысла не увмдел.
    }
}
