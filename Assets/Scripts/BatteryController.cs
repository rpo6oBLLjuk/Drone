using UnityEngine;
using CustomInspector;
using Cysharp.Threading.Tasks;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float maxPower;
    [SerializeField, DynamicSlider] private DynamicSlider currentPower;
    [SerializeField] private float consumptionSpeed;
    
    [SerializeField] private Gradient batteryColor;
    [SerializeField] private Material batteryMat;

    private void Start()
    {
        BatteryConsumption();
    }

    private void BatteryConsumption()
    {
        currentPower.value -= consumptionSpeed * Time.deltaTime;
        batteryMat.color = batteryColor.Evaluate(currentPower / maxPower);
    }
}
