using CustomInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneRecorder : MonoBehaviour
{
    [SerializeField] private Transform drone;
    [SerializeField, FixedValues(1, 2, 4, 8, 16)] private int recordUpdateFrequency;
    [SerializeField, ReadOnly] private int recordsCount;

    [SerializeField] private List<RecordedData> recordedData = new();

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
            recordedData.Add(new RecordedData(drone.position, drone.rotation));
            recordsCount = recordedData.Count;

            currentUpdateCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < recordedData.Count - 1; i++)
        {
            Gizmos.color = positionLineColor;
            Gizmos.DrawLine(recordedData[i].Position, recordedData[i + 1].Position);

            Gizmos.color = rotationLineColor;
            Gizmos.DrawLine(recordedData[i].Position + recordedData[i].Rotation * Vector3.forward * rotationOffset,
                recordedData[i + 1].Position + recordedData[i + 1].Rotation * Vector3.forward * rotationOffset);
        }
    }
}

[Serializable]
public class RecordedData
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float rotationW;

    public Vector3 Position
    {
        get => new Vector3(positionX, positionY, positionZ);
    }

    public Quaternion Rotation
    {
        get => new Quaternion(rotationX, rotationY, rotationZ, rotationW);
    }

    public RecordedData(float positionX, float positionY, float positionZ, float rotationX, float rotationY, float rotationZ, float rotationW)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
        this.rotationX = rotationX;
        this.rotationY = rotationY;
        this.rotationZ = rotationZ;
        this.rotationW = rotationW;
    }

    public RecordedData(Vector3 position, Quaternion rotation)
    {
        this.positionX = position.x;
        this.positionY = position.y;
        this.positionZ = position.z;
        this.rotationX = rotation.x;
        this.rotationY = rotation.y;
        this.rotationZ = rotation.z;
        this.rotationW = rotation.w;
    }
}