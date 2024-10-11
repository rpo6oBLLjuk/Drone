using UnityEngine;

public class AuthenticationController : MonoBehaviour
{
    [SerializeField] private InputFieldCotroller loginInputField;
    [SerializeField] private InputFieldCotroller passwordInputField;

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

        bool isVerified = await DBService.VerifyLoginAsync(loginInputField.GetText(), passwordInputField.GetText());

        if (isVerified)
        {
            Debug.Log("����� �������");
        }
        else
        {
            Debug.Log("������ ������");
        }

        loginning = false;
    }
}
