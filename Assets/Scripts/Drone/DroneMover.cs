using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneMover : MonoBehaviour, IDroneInputUser
    {
        public DroneInput DroneInput { get; set; }

        [SerializeField, ForceFill] private CameraService cameraServise;
        [SerializeField, ForceFill] private Rigidbody rb;
        [SerializeField] private Transform movedTransform;

        [SerializeField, ForceFill] private UISpeedWidget speedWidget; //Bad, требуется вынос во вне

        [HorizontalLine("Speed settings")]
        [SerializeField] private AnimationCurve asseleration;
        [SerializeField] private float maxSpeed;

        [SerializeField, ReadOnly] private Vector3 currentMovement;
        [SerializeField, ProgressBar(0, nameof(maxSpeed), isReadOnly = true)] private float currentSpeed;

        void Update()
        {
            float inputValue = asseleration.Evaluate(DroneInput.Drone.Throttle.ReadValue<float>());

            currentMovement = inputValue * maxSpeed * movedTransform.up;

            currentSpeed = Mathf.Abs(inputValue * maxSpeed);
            speedWidget.SetCurrentSpeed(currentSpeed, maxSpeed);

            cameraServise.SetCameraFov(rb.velocity.magnitude, maxSpeed);
        }

        void FixedUpdate()
        {
            rb.AddForce(currentMovement, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}

