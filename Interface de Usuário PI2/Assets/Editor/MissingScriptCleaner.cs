using UnityEngine;
using UnityEditor;

public class MissingScriptCleaner : EditorWindow
{
    [MenuItem("Tools/Clean Missing Scripts")]
    static void Init()
    {
        MissingScriptCleaner window = (MissingScriptCleaner)GetWindow(typeof(MissingScriptCleaner));
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Clean Missing Scripts in Prefabs"))
        {
            CleanMissingScriptsInPrefabs();
        }
    }

    static void CleanMissingScriptsInPrefabs()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string path in prefabPaths)
        {
            if (path.EndsWith(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                RemoveMissingScripts(prefab);
            }
        }
        AssetDatabase.SaveAssets();
        Debug.Log("Cleaned missing scripts from prefabs.");
    }

    static void RemoveMissingScripts(GameObject go)
    {
        Component[] components = go.GetComponentsInChildren<Component>(true);
        foreach (Component comp in components)
        {
            if (comp == null)
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                break;
            }
        }
    }
}
