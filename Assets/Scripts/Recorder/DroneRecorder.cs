using CustomInspector;
using System.Collections.Generic;
using UnityEngine;

public class DroneRecorder : MonoBehaviour
{
    [SerializeField] private Transform drone;
    [SerializeField, FixedValues(1, 2, 4, 8, 16)] private int recordUpdateFrequency;
    [SerializeField, ReadOnly] private int recordsCount;

    [SerializeField] private List<(Vector3, Quaternion)> recordedData = new();

    [SerializeField] private bool drawRecord;
    [SerializeField, ShowIf(nameof(drawRecord))] private float rotationOffset;
    [SerializeField, ShowIf(nameof(drawRecord))] private Color positionLineColor;
    [SerializeField, ShowIf(nameof(drawRecord))] private Color rotationLineColor;

    private int currentUpdateCount = 0;


    private void Start()
    {
        recordedData.Clear();
    }

    void Update()
    {
        currentUpdateCount++;
        if (currentUpdateCount == recordUpdateFrequency)
        {
            recordedData.Add((drone.position, drone.rotation));
            recordsCount = recordedData.Count;

            currentUpdateCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < recordedData.Count - 1; i++)
        {
            Gizmos.color = positionLineColor;
            Gizmos.DrawLine(recordedData[i].Item1, recordedData[i + 1].Item1);

            Gizmos.color = rotationLineColor;
            Gizmos.DrawLine(recordedData[i].Item1 + recordedData[i].Item2 * Vector3.forward * rotationOffset,
                recordedData[i + 1].Item1 + recordedData[i + 1].Item2 * Vector3.forward * rotationOffset);
        }
    }
}