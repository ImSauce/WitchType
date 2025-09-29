using UnityEditor;
using UnityEngine;
using System.Linq;

public class SceneAutoAdder
{
    [MenuItem("Tools/Build/Add All Scenes In Folder")]
    public static void AddAllScenesInFolder()
    {
        // 👇 Change this to the folder you want scanned
        string folderPath = "Assets/Scenes/MAIN_SCENES";

        // find all .unity files inside the folder
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { folderPath });
        string[] scenePaths = sceneGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();

        if (scenePaths.Length == 0)
        {
            Debug.LogWarning("⚠️ No scenes found in: " + folderPath);
            return;
        }

        // set them as build scenes
        EditorBuildSettings.scenes = scenePaths
            .Select(path => new EditorBuildSettingsScene(path, true))
            .ToArray();

        Debug.Log("✅ Added " + scenePaths.Length + " scenes from " + folderPath + " to Build Settings.");
    }
}