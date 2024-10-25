using UnityEngine;

public class LoginSwitcher : MonoBehaviour
{
    [SerializeField] private Transform registration;
    [SerializeField] private Transform authentication;
    [SerializeField] private bool isRegistration = true;

    private void Start()
    {
        SetObjActive();
    }

    public void ButtonClick()
    {
        isRegistration = !isRegistration;
        SetObjActive();
    }

    private void SetObjActive()
    {
        registration.gameObject.SetActive(isRegistration);
        authentication.gameObject.SetActive(!isRegistration);
    }
}
