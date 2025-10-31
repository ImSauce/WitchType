using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueHistoryTracker : MonoBehaviour
{
    private HashSet<ActorSO> spokenNPCS = new HashSet<ActorSO>();
    public void RecordNPC(ActorSO actorSO)
    {
        spokenNPCS.Add(actorSO);
        Debug.Log("Recorded NPC: " + actorSO.actorName);
    }

    public bool HasSpokenWith(ActorSO actorSO)
    {
        return spokenNPCS.Contains(actorSO);
    }
}
