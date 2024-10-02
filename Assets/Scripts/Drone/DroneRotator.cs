using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneRotator : MonoBehaviour, IDroneInputUser
    {
        [SerializeField, ForceFill] private Rigidbody rb;
        [SerializeField] private Transform rotatedTransform;

        public DroneInput DroneInput
        {
            get => droneInput;
            set => droneInput = value;
        }
        public DroneInput droneInput;

        [SerializeField, Tab("Speed")] private float rotateSpeed;
        [SerializeField, Tab("Speed")] private float angularRotateSpeed;
        [SerializeField, Tab("Angle")] private float rightAngle;
        [SerializeField, Tab("Angle")] private float verticalAngle;
        [SerializeField, Tab("Settings")] private AnimationCurve rotateCurve;
        [SerializeField, Tab("Settings")] private float rotateEndDuration;

        [SerializeField, ReadOnly] private float currentRightAngle;
        [SerializeField, ReadOnly] private float currentForvardAngle;

        private float verticalAxis;
        private float horizontalAxis;

        private bool onCollision = false;

        void Update()
        {
            if (onCollision)
                return;

            Vector2 input = DroneInput.Drone.Rotate.ReadValue<Vector2>();
            verticalAxis = input.y;
            horizontalAxis = input.x;

            //Горизонтальный отклик инвертирован для корректности вращения относительно инпута
            currentRightAngle = -horizontalAxis * rightAngle;

            currentForvardAngle = verticalAxis * verticalAngle;

            //rotatedTransform.localRotation = Quaternion.Slerp(rotatedTransform.localRotation, Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle), 1);
            //rotatedTransform.localRotation = Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle);
            Quaternion newRotation = Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, newRotation, rotateEndDuration));
        }

        void FixedUpdate()
        {
            rb.angularVelocity = angularRotateSpeed * horizontalAxis * Vector3.up;
        }

        private void OnCollisionEnter(Collision collision) => onCollision = true;
        private void OnCollisionExit(Collision collision) => onCollision = false;
    }
}

