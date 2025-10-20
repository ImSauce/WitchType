using UnityEngine;

public class ObjectiveEnemyTrigger : MonoBehaviour
{
    public ObjectiveOS objectiveToComplete;

    private void OnDestroy()
    {
        objectiveToComplete.CompleteObjective();
    }
}
