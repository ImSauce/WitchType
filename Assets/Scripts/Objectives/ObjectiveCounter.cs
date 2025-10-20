using UnityEngine;

public class ObjectiveCounter : MonoBehaviour
{
    public ObjectiveOS objectiveToComplete;
    public int targetCount;
    private int current = 0;

    public void RegisterKill()
    {
        current++;
        if (current >= targetCount)
            objectiveToComplete?.CompleteObjective();
    }
}
