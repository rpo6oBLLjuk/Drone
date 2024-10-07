using CustomInspector;
using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public abstract class IUIWidget
{
    public CanvasGroup Widget
    {
        get => _widget;
        set => _widget = value;
    }
    [SerializeField] private CanvasGroup _widget;

    [SerializeField] protected float animDuration;

    [SerializeField, ReadOnly] public bool canBeShow = true;


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
}
