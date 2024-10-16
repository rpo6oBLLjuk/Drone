using Cysharp.Threading.Tasks;
using UnityEngine;

public class RegistrationController : MonoBehaviour
{
    [SerializeField] private InputFieldController loginInputField;
    [SerializeField] private InputFieldController passwordInputField1;
    [SerializeField] private InputFieldController passwordInputField2;

    private bool loginning = false;

    public async void SingUp()
    {
        if (loginning)
        {
            Debug.Log("��� � �������� �����������");
            return;
        }

        if (!loginInputField.ValidateText())
        {
            Debug.Log("���� ������ �����������");
            return;
        }
        if (!passwordInputField1.ValidateText())
        {
            Debug.Log("���� ������ 1 �����������");
            return;
        }
        if (!passwordInputField2.ValidateText())
        {
            Debug.Log("���� ������ 2 �����������");
            return;
        }

        if (passwordInputField1.GetText() != passwordInputField2.GetText())
        {
            DBService.instance.popupService.ShowPopup("Passwords are different", PopupType.ok, false);
            return;
        }

        loginning = true;

        (bool success, string error) = await DBService.instance.RegisterUserAsync(loginInputField.GetText(), passwordInputField1.GetText());

        loginning = false;

        await UniTask.SwitchToMainThread();
        DBService.instance.popupService.ShowPopup(error, PopupType.ok, success);
    }
}
