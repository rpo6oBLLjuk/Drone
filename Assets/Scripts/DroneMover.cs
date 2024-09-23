using CustomInspector;
using UnityEngine;

namespace Drone
{
    public class DroneMover : MonoBehaviour
    {
        [SerializeField, SelfFill] private Rigidbody rb;

        [SerializeField] private float moveSpeed;
        [SerializeField, Range(0, 1)] private float assereraion;

        [SerializeField] private float rotateSpeed;

        [SerializeField] private float upSpeed;

        //bad
        private void Start()
        {
            Cursor.visible = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                rb.useGravity = false;
            else if (Input.GetKeyUp(KeyCode.Q))
                rb.useGravity = true;

            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");

            float horizontalMouseMove = Input.GetAxis("Mouse X");
            float verticalMouseMove = Input.GetAxis("Mouse Y");

            rb.AddForce(moveSpeed * verticalAxis * transform.forward * Time.deltaTime +
              horizontalAxis * moveSpeed * transform.right * Time.deltaTime, ForceMode.Force);

            transform.Rotate(new Vector3(0, horizontalMouseMove * rotateSpeed * Time.deltaTime, 0));
        }
    }
}

