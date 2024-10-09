using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;
using CustomInspector;

[Serializable]
public class GameEndWidget : IUIWidget
{
    [SerializeField] private float animYOffset;
    [SerializeField, Unit("s")] private float intervalBeforeShowWidget;
    [HorizontalLine("Level state string")]
    [SerializeField] private TextMeshProUGUI levelState;
    [SerializeField] private string winString;
    [SerializeField] private string destroyString;


    private Sequence mySequence;


    public void SetLevelState(bool isWin)
    {
        if (isWin)
        {
            levelState.text = winString;
        }
        else
        {
            levelState.text = destroyString;
        }
    }

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
            SceneManager.LoadScene(0); //Вреенная заглушка
    }

    public override void Dispose()
    {
        mySequence.Kill();
    }
}
