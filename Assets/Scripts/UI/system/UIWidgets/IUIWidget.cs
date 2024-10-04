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

    public bool HaveContent => _haveContent;
    [SerializeField] private bool _haveContent;

    public void ShowWidget(Action myAction = null)
    {
        Widget.interactable = true;
        Widget.DOFade(1, 0.5f)
            .SetUpdate(true)
            .OnComplete(() => myAction?.Invoke());

        if (_haveContent)
            SetFocus();
    }
    public void HideWidget(Action myAction = null)
    {
        Widget.interactable = false;
        Widget.DOFade(0, 0.5f)
            .SetUpdate(true)
            .OnComplete(() => myAction?.Invoke());
    }

    private void SetFocus()
    {

    }
}
