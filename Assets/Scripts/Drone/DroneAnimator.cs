using System.Collections.Generic;
using UnityEngine;

public class DroneAnimator : MonoBehaviour, IDroneInputUser
{
    public List<Transform> screws;
    public float rotatesInSecond;
    public DroneInput DroneInput { get; set; }

    void Update()
    {
        if (DroneInput.Drone.Up.IsPressed())
        {
            RotateScrews(rotatesInSecond * Time.deltaTime);
        }
        else if (DroneInput.Drone.Down.IsPressed())
        {
            RotateScrews(-rotatesInSecond * Time.deltaTime);
        }
    }

    private void RotateScrews(float angle)
    {
        foreach (Transform t in screws)
        {
            t.Rotate(new Vector3(0, 0, angle), Space.Self);
        }
    }
}
