using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Tooltip("Match this ID with a SceneTrigger's spawnPointID")]
    public string spawnPointID;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);

        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, $"Spawn: {spawnPointID}");
    }
#endif
}