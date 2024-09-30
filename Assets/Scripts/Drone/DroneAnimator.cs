using System.Collections.Generic;
using UnityEngine;

public class DroneAnimator : MonoBehaviour, IDroneInputUser
{
    public List<Transform> screws;
    public float rotatesInSecond;
    public DroneInput DroneInput { get; set; }

    void Update()
    {
        float inputValue = DroneInput.Drone.Throttle.ReadValue<float>();

        RotateScrews(rotatesInSecond * Time.deltaTime * Mathf.Sign(inputValue));
    }

    private void RotateScrews(float angle)
    {
        foreach (Transform t in screws)
        {
            t.Rotate(new Vector3(0, 0, angle), Space.Self);
        }
    }
}
