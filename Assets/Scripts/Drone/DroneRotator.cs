using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneRotator : MonoBehaviour
    {
        [SerializeField, ForceFill] private Rigidbody rb;
        [SerializeField] public DroneInput droneInput; // Bad, требуется вынос во вне
        [SerializeField] private Transform rotatedTransform;

        [HorizontalLine("Mouse settings")]
        [SerializeField] private float mouseRotateSpeed;

        [HorizontalLine("Rotate settings")]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float angularRotateSpeed;
        [SerializeField] private float rightAngle;
        [SerializeField] private float forvardAngle;
        [SerializeField] private AnimationCurve rotateCurve;

        [SerializeField, ReadOnly] private float currentRightAngle;
        [SerializeField, ReadOnly] private float currentForvardAngle;

        // Bad, требуется вынос во вне
        private void Awake()
        {
            droneInput = new();
            droneInput.Enable();

            Cursor.visible = false;
        }

        void FixedUpdate()
        {
            Vector2 input = droneInput.Drone.Rotate.ReadValue<Vector2>();
            float verticalAxis = input.y;
            float horizontalAxis = input.x;

            if (verticalAxis != 0f || horizontalAxis != 0f)
            {
                //Горизонтальный отклик инвертирован для корректности вращения относительно инпута
                currentRightAngle = Mathf.Clamp(
                    currentRightAngle -= horizontalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentRightAngle) / rightAngle),
                    -rightAngle,
                    rightAngle);
                currentForvardAngle = Mathf.Clamp(
                    currentForvardAngle += verticalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentForvardAngle) / forvardAngle),
                    -forvardAngle,
                    forvardAngle);

                rb.angularVelocity = (Vector3.up * horizontalAxis * angularRotateSpeed);

                rotatedTransform.localRotation = Quaternion.Slerp(rotatedTransform.localRotation, Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, 0), 1);
            }
            else if (horizontalAxis == 0)
                rb.angularVelocity = Vector3.zero;
        }

        
    }
}

