using UnityEngine;

public class UIService : MonoBehaviour
{
    [SerializeField] private UISpeedWidget speedWidget = new();
    [SerializeField] private LevelTimeRecorderWidget levelTimeRecorderWidget = new();

    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        speedWidget.SetCurrentSpeed(speed, maxSpeed);
    }

    public void AddTime(float time, int checkpointNumber)
    {
        levelTimeRecorderWidget.AddTime(time, checkpointNumber);
    }
}
