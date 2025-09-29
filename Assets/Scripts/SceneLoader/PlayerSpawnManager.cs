using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static string nextSpawnPointID;

    private void Start()
    {
        if (string.IsNullOrEmpty(nextSpawnPointID))
            return;

        SpawnPoint[] spawnPoints = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        foreach (var sp in spawnPoints)
        {
            if (sp.spawnPointID == nextSpawnPointID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = sp.transform.position;
                }
                break;
            }
        }
    }
}
