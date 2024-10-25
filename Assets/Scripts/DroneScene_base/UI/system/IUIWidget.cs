using CustomInspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[Serializable]
public abstract class IUIWidget : IDisposable
{
    public CanvasGroup Widget
    {
        get => _widget;
        set => _widget = value;
    }
    [HorizontalLine("IUIWidget fields"), SerializeField] private CanvasGroup _widget;

    [SerializeField, Unit("s")] protected float animDuration = 0.25f;

    [SerializeField] private bool canBeQuit = false;
    [SerializeField, Unit("s"), ShowIf(nameof(canBeQuit))] private float quitDuration = 1;
    [SerializeField, ShowIf(nameof(canBeQuit))] private Image quitFiller;

    [SerializeField, ReadOnly] public bool canBeShow = true;
    [SerializeField] protected bool isInteractible = true; //Может быть использовано для проверки активности виджета

    private CancellationTokenSource quitSource = new();

    public event Action showCompleted;
    public event Action hideCompleted;


    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Dispose()
    {

    }

    public void InjectDependencies(DiContainer container)
    {
        container.Inject(this);
    }

    public virtual void ShowWidget()
    {
        if (canBeShow)
        {
            Widget.interactable = true;

            Widget.DOFade(1, animDuration)
                .SetUpdate(true)
                .OnComplete(() => showCompleted?.Invoke());

            showCompleted?.Invoke();
        }
    }
    public virtual void HideWidget()
    {
        Widget.interactable = false;

        Widget.DOFade(0, animDuration)
            .SetUpdate(true)
            .OnComplete(() => hideCompleted?.Invoke());
    }

    public virtual void Apply()
    {

    }
    public virtual void Cancel()
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
    private async UniTask Quiting()
    {
        try
        {
            quitSource = new();
            float quitTime = 0;

            quitFiller.enabled = true;

            while (quitTime < quitDuration)
            {
                await UniTask.Yield(quitSource.Token);

                quitTime += Time.unscaledDeltaTime;
                quitFiller.fillAmount = quitTime;
            }
        }
        finally
        {
            quitFiller.enabled = false;
        }

        QuitComplete();
    }
}
