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


    public virtual void ShowWidget()
    {
        Widget.interactable = true;
        Widget.DOFade(1, 0.25f)
            .SetUpdate(true);
    }
    public virtual void HideWidget()
    {
        Widget.interactable = false;
        Widget.DOFade(0, 0.25f)
            .SetUpdate(true);
    }
}
