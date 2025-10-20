using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DialogueManager DialogueManager;
    public DialogueHistoryTracker DialogueHistoryTracker;
    public LocationHistoryTracker LocationHistoryTracker;   

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    [Header("Destroy Objects gameplay → main menu")]
    public GameObject[] groupA;
    [Header("Destroy Objects main menu → gameplay")]
    public GameObject[] groupB;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistenceObjects();
        }
        else if (Instance != null)
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
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
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

    
    public void DestroyGroupA()
    {
        DestroyPersistentObjects(groupA);
    }

    public void DestroyGroupB()
    {
        DestroyPersistentObjects(groupB);
    }

}
