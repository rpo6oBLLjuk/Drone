using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneMover : MonoBehaviour
    {
        [SerializeField, SelfFill] private Rigidbody rb;
        [SerializeField] public DroneInput droneInput; // Bad, требуется вынос во вне
        [SerializeField] private Transform movedTransform;
        [SerializeField, ForceFill] private UISpeedWidget speedWidget; //Bad, требуется вынос во вне

        [HorizontalLine("Speed settings")]
        [SerializeField] private float asseleration;
        [SerializeField] private float maxSpeed;

        [SerializeField] private Vector3 currentMovement;
        [SerializeField] private float currentSpeed;

        // Bad, требуется вынос во вне
        private void Awake()
        {
            droneInput = new();
            droneInput.Enable();

            Cursor.visible = false;
        }

        void Update()
        {
            if (droneInput.Drone.Up.IsPressed())
            {
                currentMovement = movedTransform.up * asseleration;
                rb.AddForce(currentMovement * Time.deltaTime, ForceMode.Acceleration);

                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            currentSpeed = rb.velocity.magnitude;

            speedWidget.SetCurrentSpeed(currentSpeed, maxSpeed);
        }
    }
}

