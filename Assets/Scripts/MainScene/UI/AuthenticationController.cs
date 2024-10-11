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
            Debug.Log("Уже в процессе аутентификации");
            return;
        }

        if (!loginInputField.ValidateText())
        {
            Debug.Log("Поле логина некорректно");
            return;
        }
        if (!passwordInputField.ValidateText())
        {
            Debug.Log("Поле пароля некорректно");
            return;
        }

        loginning = true;

        bool isVerified = await DBService.VerifyLoginAsync(loginInputField.GetText(), passwordInputField.GetText());

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
