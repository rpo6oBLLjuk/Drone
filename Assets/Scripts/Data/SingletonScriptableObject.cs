using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    protected virtual void OnEnable()
    {
        DestroyAsset().Forget();
    }

    //Метод нужен за тем, что "применение имени файла" от Unity происходит позже чем OnEnable, и вечно вылетает NullRef
    private async UniTask DestroyAsset()
    {
        await UniTask.Yield();

        T[] instances = Resources.FindObjectsOfTypeAll<T>();

        if (instances.Length > 1)
        {
            string assetPath = AssetDatabase.GetAssetPath(this);

            if (!string.IsNullOrEmpty(assetPath))
            {
                List<T> instanceList = instances.ToList();
                instanceList.Remove((T)(object)this);
                instances = instanceList.ToArray();

                string instancedAssetPath = AssetDatabase.GetAssetPath(instances[0]);

                EditorUtility.DisplayDialog(
                    "Ошибка создания ScriptableObject",
                    $"Файл данных для типа {typeof(T).Name} уже создан по пути: {instancedAssetPath}",
                    "ОК");
                AssetDatabase.DeleteAsset(assetPath);
            }
        }
    }
}
