using Cysharp.Threading.Tasks;
using UnityEngine;

public class AuthenticationController : MonoBehaviour
{
    [SerializeField] private InputFieldController loginInputField;
    [SerializeField] private InputFieldController passwordInputField;

    private bool loginning = false;

    public async void LogIn()
    {
        if (loginning)
        {
            Debug.Log("��� � �������� ��������������");
            return;
        }

        if (!loginInputField.ValidateText())
        {
            Debug.Log("���� ������ �����������");
            return;
        }
        if (!passwordInputField.ValidateText())
        {
            Debug.Log("���� ������ �����������");
            return;
        }

        loginning = true;

        (bool success, string error) = await DBService.instance.VerifyLoginAsync(loginInputField.GetText(), passwordInputField.GetText());

        loginning = false;

        await UniTask.SwitchToMainThread();
        DBService.instance.popupService.ShowPopup(error, PopupType.ok, success);
    }
}
