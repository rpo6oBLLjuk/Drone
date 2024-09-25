using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneMover : MonoBehaviour, IDroneInputUser
    {
        public DroneInput DroneInput
        {
            get => droneInput;
            set => droneInput = value;
        }
        public DroneInput droneInput;

        [SerializeField, ForceFill] private CameraService cameraServise;
        [SerializeField, ForceFill] private Rigidbody rb;
        [SerializeField] private Transform movedTransform;

        [SerializeField, ForceFill] private UISpeedWidget speedWidget; //Bad, требуется вынос во вне

        [HorizontalLine("Speed settings")]
        [SerializeField] private float asseleration;
        [SerializeField] private float maxSpeed;

        [SerializeField, ReadOnly] private Vector3 currentMovement;
        [SerializeField, ProgressBar(0, nameof(maxSpeed), isReadOnly = true)] private float currentSpeed;

        void FixedUpdate()
        {
            if (droneInput.Drone.Up.IsPressed())
                currentMovement = movedTransform.up * asseleration;
            else if (droneInput.Drone.Down.IsPressed())
                currentMovement = -1 * asseleration * movedTransform.up;
            else
                currentMovement = Vector3.zero;

            currentSpeed = rb.velocity.magnitude;
            speedWidget.SetCurrentSpeed(currentSpeed, maxSpeed);

            cameraServise.SetCameraFov(currentSpeed, maxSpeed);

            rb.AddForce(currentMovement, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

    }
}

