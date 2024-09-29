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

        [SerializeField, Tab("Speed")] private float rotateSpeed;
        [SerializeField, Tab("Speed")] private float angularRotateSpeed;
        [SerializeField, Tab("Angle")] private float rightAngle;
        [SerializeField, Tab("Angle")] private float forvardAngle;
        [SerializeField, Tab("Settings")] private AnimationCurve rotateCurve;
        [SerializeField, Tab("Settings")] private float rotateEndDuration;

        [SerializeField, ReadOnly] private float currentRightAngle;
        [SerializeField, ReadOnly] private float currentForvardAngle;

        private float verticalAxis;
        private float horizontalAxis;

        //private bool horizontalInputStoped;

        // Bad, ��������� ����� �� ���
        private void Awake()
        {
            Cursor.visible = false;
        }

        void Update()
        {
            Vector2 input = DroneInput.Drone.Rotate.ReadValue<Vector2>();
            verticalAxis = input.y;
            horizontalAxis = input.x;

            //�������������� ������ ������������ ��� ������������ �������� ������������ ������
            currentRightAngle = Mathf.Clamp(
                currentRightAngle -= horizontalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentRightAngle) / rightAngle),
                -rightAngle,
                rightAngle);

            currentForvardAngle = Mathf.Clamp(
                    currentForvardAngle += verticalAxis * rotateSpeed * Time.deltaTime * rotateCurve.Evaluate(Mathf.Abs(currentForvardAngle) / forvardAngle),
                    -forvardAngle,
                    forvardAngle);

            //rotatedTransform.localRotation = Quaternion.Slerp(rotatedTransform.localRotation, Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle), 1);
            rotatedTransform.localRotation = Quaternion.Euler(currentForvardAngle, rotatedTransform.localRotation.eulerAngles.y, currentRightAngle);
        }

        void FixedUpdate()
        {
            rb.angularVelocity = angularRotateSpeed * horizontalAxis * Vector3.up;
        }
    }
}

