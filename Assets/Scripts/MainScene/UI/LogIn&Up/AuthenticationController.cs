using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AuthenticationController : MonoBehaviour
{
    [SerializeField] private InputFieldController loginInputField;
    [SerializeField] private InputFieldController passwordInputField;

    private TaskCompletionSource<bool> authTask;

    public async void LogIn()
    {
        if (authTask != null && !authTask.Task.IsCompleted)
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

        authTask = new();

        (bool success, string error) = await DBService.instance.VerifyLoginAsync(loginInputField.GetText(), passwordInputField.GetText());

        if (!success)
            await UniTask.SwitchToMainThread();

        DBService.instance.popupService.ShowPopup(error, PopupType.ok, success);

        authTask.SetResult(success);
    }
}
