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
            Debug.Log("Уже в процессе регистрации");
            return;
        }

        if (!loginInputField.ValidateText())
        {
            Debug.Log("Поле логина некорректно");
            return;
        }
        if (!passwordInputField1.ValidateText())
        {
            Debug.Log("Поле пароля 1 некорректно");
            return;
        }
        if (!passwordInputField2.ValidateText())
        {
            Debug.Log("Поле пароля 2 некорректно");
            return;
        }

        if (passwordInputField1.GetText() != passwordInputField2.GetText())
        {
            Debug.Log("Введённые пароли различаются");
            return;
        }

        loginning = true;

        bool isVerified = await DBService.RegisterUserAsync(loginInputField.GetText(), passwordInputField1.GetText());

        if (isVerified)
        {
            Debug.Log("Логин успешен");
        }
        else
        {
            Debug.Log("Ошибка логина");
        }

        loginning = false;
    }

}
