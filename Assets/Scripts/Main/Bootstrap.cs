using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private bool cursorVisible;

    public void Awake()
    {
        Cursor.visible = cursorVisible;
        DroneInput droneInput = new();
        droneInput.Enable();

        foreach (IDroneInputUser user in FindObjectsOfType<MonoBehaviour>().Where((thisClass)=> thisClass is IDroneInputUser))
        {
            user.DroneInput = droneInput;
        }
    }
}
