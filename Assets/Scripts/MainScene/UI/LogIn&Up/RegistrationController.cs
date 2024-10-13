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
            DBService.instance.popupService.ShowPopup("Passwords are different", PopupType.ok, false);
            return;
        }

        loginning = true;

        (bool, string) verifyData = await DBService.instance.RegisterUserAsync(loginInputField.GetText(), passwordInputField1.GetText());

        DBService.instance.popupService.ShowPopup(verifyData.Item2, PopupType.ok, verifyData.Item1);

        loginning = false;
    }

}
