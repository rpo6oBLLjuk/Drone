using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private bool cursorVisible;

    public void Awake()
    {
        Cursor.visible = cursorVisible;
    }
}
