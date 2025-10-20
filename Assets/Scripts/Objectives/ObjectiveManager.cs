using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    public ObjectiveList currentObjectiveList;
    public TMP_Text objectiveText;
    public Canvas objectivePanel;
    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(InitializeObjectives());
    }

    private IEnumerator InitializeObjectives()
    {
        yield return null; 

        if (currentObjectiveList == null)
        {
            if (objectiveText != null)
                objectiveText.text = "";
            yield break;
        }

        StartObjective(currentIndex);
    }

    private void StartObjective(int index)
    {
        if (index >= currentObjectiveList.objectives.Length)
        {
            if (objectiveText != null)
                objectiveText.text = "All objectives completed!";

            ShowObjective();
            return;
        }

        var obj = currentObjectiveList.objectives[index];
        obj.StartObjective();

        if (DialogueManager.Instance != null)
            DialogueManager.Instance.StartDialogue(obj.dialogueSO);

        if (objectiveText != null)
            objectiveText.text = obj.description;

        ShowObjective();
        obj.onComplete.AddListener(HandleObjectiveCompleted);
    }

    private void HandleObjectiveCompleted()
    {
        var current = currentObjectiveList.objectives[currentIndex];
        current.onComplete.RemoveListener(HandleObjectiveCompleted);

        currentIndex++;
        StartObjective(currentIndex);
    }

    public void CompleteCurrentObjective()
    {
        if (currentIndex < currentObjectiveList.objectives.Length)
            currentObjectiveList.objectives[currentIndex].CompleteObjective();
    }

    private void ShowObjective()
    {
        if (objectivePanel != null)
            objectivePanel.gameObject.SetActive(true);
    }
}
