using CustomInspector;
using Drone;
using UnityEngine;

public class BatteryController : MonoBehaviour, IDroneInputUser
{
    public DroneInput DroneInput
    {
        get => droneInput;
        set => droneInput = value;
    }
    private DroneInput droneInput;

    [SerializeField] private float maxPower;
    [SerializeField, ProgressBar(0, nameof(maxPower))] private float currentPower;
    [SerializeField] private float consumptionSpeed;

    [SerializeField] private Gradient batteryColor;
    [SerializeField] private Material batteryMat;

    private void Start()
    {
        currentPower = maxPower;
    }

    private void Update()
    {
        if (droneInput.Drone.Throttle.IsPressed())
            BatteryConsumption();
    }

    private void BatteryConsumption()
    {
        if (currentPower < 0)
        {
            return;
        }

        currentPower -= consumptionSpeed * Time.deltaTime;
        batteryMat.color = batteryColor.Evaluate(currentPower / maxPower);
    }
}
