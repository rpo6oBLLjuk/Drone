using UnityEngine;
using UnityEngine.UI;

public class UISpeedWidget : MonoBehaviour
{
    public Image image;

    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        image.fillAmount = speed / maxSpeed;
    }
}
