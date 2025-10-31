using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();

    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective)
    {
        if(!questProgress.ContainsKey(questSO))
        {
            questProgress[questSO] = new Dictionary<QuestObjective, int>();
        }

        var progressDictionary = questProgress[questSO];

        int newAmount = 0;

       // if we have items for quest
       if(objective.targetLocation != null && GameManager.Instance.LocationHistoryTracker.HasVisited(objective.targetLocation))
        {
            newAmount = objective.requiredAmount;
        }
       else if(objective.targetNPC != null && GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(objective.targetNPC))
        {
            newAmount = objective.requiredAmount;
        }

        progressDictionary[objective] = newAmount;
    }
    public string GetProgressText(QuestSO questSO,QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);
        
        if(currentAmount >= objective.requiredAmount)
        {
            return "Complete";
        }
        else
        {
            return "In Progress";
        }

    } 

    public int GetCurrentAmount(QuestSO questoSO, QuestObjective objective)
    {
        if (questProgress.TryGetValue(questoSO, out var objectiveDictionary))
            if (objectiveDictionary.TryGetValue(objective, out int amount))
                return amount;
        return 0;
    }

}
