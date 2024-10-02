using System;
using TMPro;
using UnityEngine;

[Serializable]
public class LevelTimeRecorderWidget
{
    [SerializeField] private GameObject baseTMPro;
    [SerializeField] private Transform parentContainer;
    [SerializeField] private string checkpointNameText = "Checkpoint: ";
    [SerializeField] private string checkpointTimeText = "Time: ";

    public void AddTime(float time, int checkpointNumber)
    {
        time = Mathf.Round(time * 1000) / 1000;

        TextMeshProUGUI newText = UnityEngine.Object.Instantiate(baseTMPro).GetComponent<TextMeshProUGUI>();

        newText.transform.SetParent(parentContainer);
        newText.text = $"{checkpointNameText}{checkpointNumber}, {checkpointTimeText}{time}";
    }
}
