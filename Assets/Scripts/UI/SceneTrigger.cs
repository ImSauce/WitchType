#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [Header("Scene To Load")]
    [SerializeField] private Object sceneAsset;

    private string sceneName;

    private void Awake()
    {
#if UNITY_EDITOR
        UpdateSceneName();
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateSceneName();
    }

    private void UpdateSceneName()
    {
        if (sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(sceneAsset);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
        else
        {
            sceneName = "";
        }
    }

    private void OnDrawGizmos()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // convert world position to GUI screen position
            Vector3 worldPos = transform.position + Vector3.up * 1.5f;
            Vector3 screenPos = HandleUtility.WorldToGUIPoint(worldPos);

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.cyan;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 10; // 👈 change this to whatever size you want

            Handles.BeginGUI();
            GUI.Label(new Rect(screenPos.x - 50, screenPos.y - 15, 100, 30), $"->: {sceneName}", style);
            Handles.EndGUI();
        }
    }
#endif
}

// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneTrigger : MonoBehaviour
// {
//     [Header("Scene To Load (Name)")]
//     public string sceneName; 

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         Debug.Log("Trigger hit: " + other.name);

//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Player entered trigger, loading scene: " + sceneName);
//             SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
//         }
//     }
// }

