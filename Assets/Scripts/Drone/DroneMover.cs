using CustomInspector;
using UnityEngine;
using Zenject;

namespace Drone
{
    public class DroneMover : MonoBehaviour, IDroneInputUser
    {
        [Inject] readonly UIService uIService;
        [Inject] readonly CameraService cameraServise;
        public DroneInput DroneInput { get; set; }

        [SerializeField, ForceFill] private Rigidbody rb;
        [SerializeField] private Transform movedTransform;

        [HorizontalLine("Speed settings")]
        [SerializeField] private AnimationCurve asseleration;
        [SerializeField] private float maxSpeed;

        [SerializeField, ReadOnly] private Vector3 currentMovement;
        [SerializeField, ReadOnly] private float inputValue;
        [SerializeField, ProgressBar(0, nameof(maxSpeed), isReadOnly = true)] private float currentSpeed;

        void Update()
        {
            float input = DroneInput.Drone.Throttle.ReadValue<float>();
            inputValue = asseleration.Evaluate(Mathf.Abs(input)) * Mathf.Sign(input);

            currentMovement = inputValue * maxSpeed * movedTransform.up;

            currentSpeed = Mathf.Abs(inputValue * maxSpeed);
            uIService.SetCurrentSpeed(currentSpeed, maxSpeed);

            cameraServise.SetCameraFov(rb.velocity.magnitude, maxSpeed);
        }

        void FixedUpdate()
        {
            rb.AddForce(currentMovement, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}

