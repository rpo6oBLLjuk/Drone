using Cysharp.Threading.Tasks;
using UnityEngine;

public class DBService : MonoBehaviour
{
    public static DBService instance; //Синглтон, не вижу смысла использовать тут Zenject (возможно, будет использован позже)

    [SerializeField] private LoadingWidget loadingWidget;


    private void Awake()
    {
        instance = this;
    }

    public async UniTask<(bool success, string error)> VerifyLoginAsync(string login, string password)
    {
        RequestEnable();
        void endAction() => RequestDisable().Forget();

        (bool success, string error) = await DBConnect.VerifyLoginAsync(login, password, endAction);

        await UniTask.SwitchToMainThread();
        PopupService.instance.ShowPopup(error, PopupType.ok, success);

        if (success)
            StateController.AuthComplete?.Invoke();

        return (success, error);
    }

    public async UniTask<(bool success, string error)> RegisterUserAsync(string login, string password)
    {
        RequestEnable();
        void endAction() => RequestDisable().Forget();

        (bool success, string error) = await DBConnect.RegisterUserAsync(login, password, endAction);

        await UniTask.SwitchToMainThread();
        PopupService.instance.ShowPopup(error, PopupType.ok, success);

        if (success)
            StateController.AuthComplete?.Invoke();

        return (success, error);
    }

    private void RequestEnable()
    {
        loadingWidget.EnableWidget();
    }

    private async UniTask RequestDisable()
    {
        await UniTask.SwitchToMainThread();

        loadingWidget.DisableWidget();
    }
}

