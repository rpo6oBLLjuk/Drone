using CustomInspector;
using Drone;
using System.Collections.Generic;
using UnityEngine;

public class DroneDestroyer : MonoBehaviour, IDroneInputUser
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField, ForceFill] private Rigidbody rb;
    [SerializeField, Layer] private int saveLayers;

    private bool isDestroy = false;

    public DroneInput DroneInput { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == saveLayers)
            return;

        if (isDestroy)
            return;

        DroneInput.Drone.Disable();
        GameStateController.End(false);

        isDestroy = true;
        Debug.Log("���� ���������");

        foreach (GameObject t in objectsToDestroy)
        {
            t.GetComponent<Collider>().enabled = true;

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