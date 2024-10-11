using UnityEngine;

public class RegistrationController : MonoBehaviour
{
    [SerializeField] private InputFieldCotroller loginInputField;
    [SerializeField] private InputFieldCotroller passwordInputField1;
    [SerializeField] private InputFieldCotroller passwordInputField2;

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
            Debug.Log("�������� ������ �����������");
            return;
        }

        loginning = true;

        bool isVerified = await DBService.RegisterUserAsync(loginInputField.GetText(), passwordInputField1.GetText());

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
