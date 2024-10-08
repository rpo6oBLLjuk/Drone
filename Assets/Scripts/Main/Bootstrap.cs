using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private bool cursorVisible;

    public void Awake()
    {
        Cursor.visible = cursorVisible;
    }

    public void Start()
    {
        GameStateController.Start();
    }

    public void OnDestroy()
    {
        GameStateController.End(false);
    }
}
