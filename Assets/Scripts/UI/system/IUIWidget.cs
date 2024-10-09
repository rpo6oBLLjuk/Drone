using CustomInspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

[Serializable]
public abstract class IUIWidget : IDisposable
{
    [Inject] protected UIService uiService;

    public CanvasGroup Widget
    {
        get => _widget;
        set => _widget = value;
    }
    [HorizontalLine("IUIWidget fields"), SerializeField] private CanvasGroup _widget;

    [SerializeField, Unit("s")] protected float animDuration = 0.25f;

    [SerializeField] private bool canBeQuit = false;
    [SerializeField, Unit("s"), ShowIf(nameof(canBeQuit))] private float quitDuration = 1;

    [SerializeField, ReadOnly] public bool canBeShow = true;
    [SerializeField, ReadOnly] protected bool canBeApply = false;

    private CancellationTokenSource quitSource = new();


    public virtual void Start()
    {

    }

    public virtual void Update()
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
                .SetUpdate(true)
                .OnComplete(() => canBeApply = true);
        }
    }
    public virtual void HideWidget()
    {
        canBeApply = false;

        Widget.interactable = false;

        Widget.DOFade(0, animDuration)
            .SetUpdate(true);
    }

    public virtual void Apply()
    {

    }

    public virtual void Options()
    {

    }

    public virtual void QuitStart()
    {
        if (canBeQuit)
        {
            Quiting().Forget();

            Debug.Log("Выход начат");
        }
    }

    public virtual void QuitPaused()
    {
        quitSource.Cancel();
        Debug.Log("Выход остановлен");
    }

    protected virtual void QuitComplete()
    {
        Debug.Log("Выход совершён");
    }

    public void InjectDependencies(DiContainer container)
    {
        container.Inject(this);

        Widget.alpha = 0; //Не лучшее решение, но виджет априори должен быть отключён при создании, и дёргать лишний метод я смысла не увмдел.
    }

    private async UniTask Quiting()
    {
        float quitTime = 0;

        while (quitTime < quitDuration)
        {
            await UniTask.Yield(quitSource.Token);
            quitTime += Time.unscaledDeltaTime;
        }

        QuitComplete();
    }
}
