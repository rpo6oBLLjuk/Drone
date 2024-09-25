using CustomInspector;
using Drone;
using System.Collections.Generic;
using UnityEngine;

public class DroneDestroyer : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField, ForceFill] private Rigidbody rb;

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
            Rigidbody newRb = t.AddComponent<Rigidbody>();
            newRb.velocity = rb.velocity;
            newRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        foreach (MonoBehaviour mono in GetComponentsInChildren<MonoBehaviour>())
        {
            mono.enabled = false;
        }
    }
}