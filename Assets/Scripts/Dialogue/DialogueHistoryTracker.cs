using UnityEngine;
using System.Collections.Generic;

public class DialogueHistoryTracker : MonoBehaviour
{
    private HashSet<ActorSO> spokenNPCS = new HashSet<ActorSO>();
    public void RecordNPC(ActorSO actorSO)
    {
        spokenNPCS.Add(actorSO);
        Debug.Log("Recorded NPC: " + actorSO.actorName);
    }

    public bool HasSpokenTo(ActorSO actorSO)
    {
        return spokenNPCS.Contains(actorSO);
    }
}
