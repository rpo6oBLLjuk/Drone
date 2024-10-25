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

    //����� ����� �� ���, ��� "���������� ����� �����" �� Unity ���������� ����� ��� OnEnable, � ����� �������� NullRef
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
                    "������ �������� ScriptableObject",
                    $"���� ������ ��� ���� {typeof(T).Name} ��� ������ �� ����: {instancedAssetPath}",
                    "��");
                AssetDatabase.DeleteAsset(assetPath);
            }
        }
    }
}
