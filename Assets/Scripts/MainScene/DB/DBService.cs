using Cysharp.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using UnityEngine;

public class DBService : MonoBehaviour
{
    public static DBService instance; //Синглтон, не вижу смысла использовать тут Zenject (возможно, будет использован позже)

    [SerializeField] public LoadingWidget loadingWidget;
    [SerializeField] public PopupService popupService;


    private void Start()
    {
        instance = this;
    }

    public async UniTask<(bool success, string error)> VerifyLoginAsync(string login, string password)
    {
        RequestEnable();
        void endAction() => RequestDisable().Forget();

        return await DBConnect.VerifyLoginAsync(login, password, endAction);
    }

    public async UniTask<(bool success, string error)> RegisterUserAsync(string login, string password)
    {
        RequestEnable();
        void endAction() => RequestDisable().Forget();

        return await DBConnect.RegisterUserAsync(login, password, endAction);
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

