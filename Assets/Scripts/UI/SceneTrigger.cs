using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneTrigger : MonoBehaviour
{
    [Header("Scene To Load")]
    [SerializeField] private Object sceneAsset; // drag your scene asset here

    private string sceneName;

    private void Awake()
    {
        #if UNITY_EDITOR
        if (sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(sceneAsset);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
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

