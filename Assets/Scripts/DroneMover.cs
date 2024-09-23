using UnityEngine;

namespace Drone
{
    public class DroneMover : MonoBehaviour
    {
        void Update()
        {
            float verticalAxis = Input.GetAxis("Vertical");
            if (verticalAxis != 0)
            {
                transform.position += transform.forward * verticalAxis;
            }
        }
    }
}

