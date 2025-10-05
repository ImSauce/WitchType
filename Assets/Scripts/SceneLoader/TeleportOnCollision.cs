using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    [Tooltip("Match this ID with a SpawnPoint's spawnPointID")]
    public string spawnPointID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("TRIGGERED");
            // Find the spawn point with the matching ID
            SpawnPoint targetSpawn = FindSpawnPointByID(spawnPointID);
            if (targetSpawn != null)
            {
                collision.gameObject.transform.position = targetSpawn.transform.position;
            }
            else
            {
                Debug.LogWarning("No SpawnPoint found with ID: " + spawnPointID);
            }
        }
    }




    private SpawnPoint FindSpawnPointByID(string id)
    {
        SpawnPoint[] allSpawns = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        foreach (SpawnPoint sp in allSpawns)
        {
            if (sp.spawnPointID == id)
                return sp;
        }
        return null;
    }
}




