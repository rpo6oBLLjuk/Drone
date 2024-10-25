using CustomInspector;
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class FPSWidget : IUIWidget
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI fixedFpsText;
    [SerializeField] private float updateCount;
    [SerializeField, ProgressBar(0, nameof(updateCount), isReadOnly = true)] private float currentUpdateTime;


    public override void Update()
    {
        currentUpdateTime += Time.unscaledDeltaTime;

        if (currentUpdateTime > 1 / updateCount)
        {
            currentUpdateTime = 0;

            fpsText.text = (Mathf.Round(1 / Time.unscaledDeltaTime * 100) / 100).ToString();
            fixedFpsText.text = (1 / Time.fixedDeltaTime).ToString();
        }
    }
}