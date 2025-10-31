using UnityEngine;
using TMPro;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    public QuestSO currentQuest;
    public QuestLogUI questLogUI;

    private void OnValidate()
    {
        if(currentQuest != null)    
        SetQuest(currentQuest);
        else
            gameObject.SetActive(false);
    }
    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        gameObject.SetActive(true);
    }

    public void OnSlotClicked()
    {
        questLogUI.HandleQuestClicked(currentQuest);
    }
}
