using DG.Tweening;
using System;
using UnityEngine;
public interface IUIWidget
{
    public CanvasGroup Widget { get; set; }

    public void ShowWidget(Action myAction = null)
    {
        Widget.interactable = true;
        Widget.DOFade(1, 0.5f)
            .SetUpdate(true)
            .OnComplete(() => myAction?.Invoke());
    }
    public void HideWidget(Action myAction = null)
    {
        Widget.interactable = false;
        Widget.DOFade(0, 0.5f)
            .SetUpdate(true)
            .OnComplete(() => myAction?.Invoke());
    }
}
