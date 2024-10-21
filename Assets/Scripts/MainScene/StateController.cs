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

    private async void LoadLevelSelectScene()
    {
        loadingWidget.EnableWidget();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelSelectSceneIndex);

        while (!asyncLoad.isDone)
        {
            await UniTask.Yield();
        }
        
        loadingWidget.DisableWidget();
        Debug.Log("—цена выбора уровн€ загружена.");

        asyncLoad.allowSceneActivation = true;
    }
}
