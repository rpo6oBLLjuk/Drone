using CustomInspector;
using Drone;
using System.Collections.Generic;
using UnityEngine;

public class DroneDestroyer : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField, SelfFill(true)] private DroneMover droneMover;
    [SerializeField, SelfFill(true)] private Rigidbody rb;

    private bool isDestroy = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isDestroy)
            return;

        isDestroy = true;
        Debug.Log("Дрон уничтожен");

        foreach (GameObject t in objectsToDestroy)
        {
            t.transform.parent = null;
            t.AddComponent<Rigidbody>().velocity = rb.velocity;
        }

        droneMover.enabled = false;
        this.enabled = false;
    }
}