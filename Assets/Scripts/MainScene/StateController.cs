using CustomInspector;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour
{
    public static Action AuthComplete;

    [SerializeField, Scene] private int levelSelectSceneIndex;
    [SerializeField] private LoadingWidget loadingWidget;

    private void OnEnable()
    {
        AuthComplete += LoadLevelSelectScene;
    }

    private void OnDisable()
    {
        AuthComplete -= LoadLevelSelectScene;
    }
    
    //Стоило бы вынести в Bootstrap
    private void Start()
    {
        if(SaveService.LoadLocalUser() != null)
        {
            PopupService.instance.ShowPopup("Local Authentication completed", PopupType.ok, true);
            LoadLevelSelectScene();
        }
    }

    private async void LoadLevelSelectScene()
    {
        loadingWidget.EnableWidget();

        await UniTask.SwitchToThreadPool();
        await DBConnect.LoadAllLevelsDataAsync();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelSelectSceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            await UniTask.Yield();
        }

        loadingWidget.DisableWidget();
        Debug.Log("Сцена выбора уровня загружена.");

        asyncLoad.allowSceneActivation = true;
    }
}
