using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{

    public int sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger, loading scene...");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}