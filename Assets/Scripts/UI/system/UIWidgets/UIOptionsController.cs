using System;
using UnityEngine;

[Serializable]
public class UIOptionsController : IUIContentWidget
{
    public override void ShowWidget()
    {
        base.ShowWidget();

        Time.timeScale = 0;
    }

    public override void HideWidget()
    {
        base.HideWidget();

        Time.timeScale = 1;
    }
}
