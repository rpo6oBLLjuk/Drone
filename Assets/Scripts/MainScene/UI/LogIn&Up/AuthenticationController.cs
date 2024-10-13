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

        (bool, string) verifyData = await DBService.instance.VerifyLoginAsync(loginInputField.GetText(), passwordInputField.GetText());

        DBService.instance.popupService.ShowPopup(verifyData.Item2, PopupType.ok, verifyData.Item1);

        loginning = false;
    }
}
