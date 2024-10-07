using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class GameEndWidget : IUIWidget
{
    [SerializeField] private float animYOffset;

    public override void ShowWidget()
    {
        base.ShowWidget();

        Vector3 position = Widget.transform.position;
        Widget.transform.DOMoveY(position.y, animDuration)
            .From(position.y - animYOffset);
    }

    public override void Apply()
    {
        SceneManager.LoadScene(0); //Вреенная заглушка
    }
}
