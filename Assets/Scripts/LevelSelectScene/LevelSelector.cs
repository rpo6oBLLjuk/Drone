using CustomInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Transform levelContainer;
    [SerializeField, Scene] private List<int> levelIndexes;

    private void Start()
    {
        try
        {
            Button[] levelButtons = levelContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < levelButtons.Length; i++)
            {
                int index = i; // —охран€ю текущее значение индекса, т.к. I к концу станет >размера списка, и будет таковым дл€ всех л€мбд
                levelButtons[index].onClick.AddListener(() => SceneManager.LoadScene(levelIndexes[index]));
            }
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
