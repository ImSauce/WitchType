using TMPro;
using UnityEngine;


public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private QuestObjectiveSlot[] objectiveSlots;

    private QuestSO questSO;




    public void HandleQuestClicked(QuestSO questSO)
    {
        this.questSO = questSO;
        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjective();
        foreach (var objective in questSO.objectives)
        {
           questManager.UpdateObjectiveProgress(questSO, objective);
           Debug.Log($"Objective: {objective.description} => {questManager.GetProgressText(questSO, objective)}");
        }
    }

    private void DisplayObjective()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if(i < questSO.objectives.Count)
            {
              var objective = questSO.objectives[i];
               questManager.UpdateObjectiveProgress(questSO, objective);

                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;

                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjective(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
