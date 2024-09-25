using CustomInspector;
using Drone;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private DroneMover droneMover;

    [SerializeField] private float maxPower;
    [SerializeField, DynamicSlider] private DynamicSlider currentPower;
    [SerializeField] private float consumptionSpeed;

    [SerializeField] private Gradient batteryColor;
    [SerializeField] private Material batteryMat;


    private void Update()
    {
        if (droneMover.droneInput.Drone.Up.IsPressed())
            BatteryConsumption();
    }

    private void BatteryConsumption()
    {
        currentPower.value -= consumptionSpeed * Time.deltaTime;
        batteryMat.color = batteryColor.Evaluate(currentPower / maxPower);

        if (currentPower.value < 0)
            droneMover.enabled = false;
    }
}
