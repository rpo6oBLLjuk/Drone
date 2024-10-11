using CustomInspector;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameEndWidget : IUIWidget
{
    [HorizontalLine("Game end fields")]
    [SerializeField] private float animYOffset;
    [SerializeField, Unit("s")] private float intervalBeforeTrueShowWidget = 0.5f;
    [SerializeField, Unit("s")] private float intervalBeforeFalseShowWidget = 2f;

    [SerializeField] private TextMeshProUGUI levelState;
    [SerializeField] private string winString;
    [SerializeField] private string destroyString;

    private float intervalBeforeShowWidget;

    private Sequence mySequence;


    public override void ShowWidget()
    {
        Vector3 position = Widget.transform.position;

        mySequence = DOTween.Sequence();

        mySequence.AppendInterval(intervalBeforeShowWidget)
            .OnComplete(() => base.ShowWidget());

        mySequence.Join(Widget.transform.DOMoveY(position.y, animDuration)
            .From(position.y - animYOffset));
    }

    public override void Apply()
    {
        if (canBeApply)
            SceneManager.LoadScene(0); //Временная заглушка
    }

    public override void Dispose()
    {
        mySequence.Kill();
    }

    public void SetLevelState(bool isWin)
    {
        if (isWin)
        {
            levelState.text = winString;
            intervalBeforeShowWidget = intervalBeforeTrueShowWidget;
        }
        else
        {
            levelState.text = destroyString;
            intervalBeforeShowWidget = intervalBeforeFalseShowWidget;
        }
    }
}
