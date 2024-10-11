using System;
using TMPro;
using UnityEngine;

[Serializable]
public class LevelTimeRecorderWidget : IUIWidget
{
    [SerializeField] private GameObject baseTMPro;
    [SerializeField] private Transform parentContainer;
    [SerializeField] private string checkpointNameText = "Checkpoint: ";
    [SerializeField] private string checkpointTimeText = "Time: ";


    public void AddTime(float time, int checkpointNumber)
    {
        string newTime = string.Format("{0:0.000}", time);

        GameObject newObj = UnityEngine.Object.Instantiate(baseTMPro);
        newObj.transform.SetParent(parentContainer);

        TextMeshProUGUI newText = newObj.GetComponentInChildren<TextMeshProUGUI>();
        newText.text = $"{checkpointNameText}{checkpointNumber}, {checkpointTimeText}{newTime}";
    }
}
