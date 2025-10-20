using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public ObjectiveOS objectiveToComplete;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (objectiveToComplete != null)
        {
            objectiveToComplete.CompleteObjective();
            gameObject.SetActive(false); 
        }
    }
}
