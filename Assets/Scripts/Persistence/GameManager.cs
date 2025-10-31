using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DialogueManager DialogueManager;
    public DialogueHistoryTracker DialogueHistoryTracker;
    public LocationHistoryTracker LocationHistoryTracker;
    public SpellBookUI spellBookUI;
    public SpellManager spellManager;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    [Header("Destroy Objects gameplay → main menu")]
    public GameObject[] groupA;

    [Header("Destroy Objects main menu → gameplay")]
    public GameObject[] groupB;

    [Header("Manual Timer Settings")]
    [Tooltip("Time (in seconds) before destroying Group A objects")]
    public float groupADestroyDelay = 1f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistenceObjects();
        }
        else
        {
            CleanUpAndDestroy();
            return;
        }
    }

    private void MarkPersistenceObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                DontDestroyOnLoad(obj);
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                Destroy(obj);
        }

        Destroy(gameObject);
    }

    public void DestroyPersistentObjects(GameObject[] objectsToDestroy)
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"Destroyed: {obj.name}");
            }
        }
    }

    // 🔹 Now has a built-in manual timer
    public void DestroyGroupA()
    {
        StartCoroutine(DestroyGroupAWithDelay());
    }

    private IEnumerator DestroyGroupAWithDelay()
    {
        Debug.Log($"Waiting {groupADestroyDelay} seconds before destroying Group A...");
        yield return new WaitForSeconds(groupADestroyDelay);
        DestroyPersistentObjects(groupA);
        Debug.Log("Group A destroyed after delay.");
    }

    public void DestroyGroupB()
    {
        DestroyPersistentObjects(groupB);
    }

    public void DestroySelf(float delay = 1f)
    {
        StartCoroutine(DestroySelfWithDelay(delay));
    }

    private IEnumerator DestroySelfWithDelay(float delay)
    {
        Debug.Log($"GameManager will destroy itself in {delay} seconds...");
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        Debug.Log("GameManager destroyed.");
    }
}
