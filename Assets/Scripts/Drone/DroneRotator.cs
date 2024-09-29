using CustomInspector;
using DG.Tweening;
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

        [HorizontalLine("Mouse settings")]
        [SerializeField] private float mouseRotateSpeed;

        [HorizontalLine("Rotate settings")]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float angularRotateSpeed;
        [SerializeField] private float rightAngle;
        [SerializeField] private float forvardAngle;
        [SerializeField] private AnimationCurve rotateCurve;
        [SerializeField] private float rotateEndDuration;

        [SerializeField, ReadOnly] private float currentRightAngle;
        [SerializeField, ReadOnly] private float currentForvardAngle;

        private float verticalAxis;
        private float horizontalAxis;

        private bool horizontalInputStoped;

        // Bad, требуется вынос во вне
        private void Awake()
        {
            Cursor.visible = false;
        }

        void Update()
        {
            Vector2 input = DroneInput.Drone.Rotate.ReadValue<Vector2>();
            verticalAxis = input.y;
            horizontalAxis = input.x;

            //Горизонтальный отклик инвертирован для корректности вращения относительно инпута
            currentRightAngle = Mathf.Clamp(
                currentRightAngle -= horizontalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentRightAngle) / rightAngle),
                -rightAngle,
                rightAngle);

            currentForvardAngle = Mathf.Clamp(
                    currentForvardAngle += verticalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentForvardAngle) / forvardAngle),
                    -forvardAngle,
                    forvardAngle);

            if (horizontalAxis != 0f)
                horizontalInputStoped = false;

            //rotatedTransform.localRotation = Quaternion.Slerp(rotatedTransform.localRotation, Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle), 1);
            rotatedTransform.localRotation = Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle);
        }

        void FixedUpdate()
        {
            rb.angularVelocity = angularRotateSpeed * horizontalAxis * Vector3.up;

            if (horizontalInputStoped == false)
            {
                horizontalInputStoped = true;
                rb.DORotate(Vector3.zero, rotateEndDuration).SetEase(Ease.OutQuad);
            }
        }
    }
}

