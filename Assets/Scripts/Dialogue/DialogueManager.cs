using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool isDialogueActive;

    [Header("UI Elements")]
    public CanvasGroup canvasGroup;
    public Image potrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
           ShowChoices();

    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];
        potrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    private void ShowChoices()
    {
        ClearChoices();
        if (currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "End";
            choiceButtons[0].onClick.AddListener(EndDialogue);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }

    private void ChooseOption(DialogueSO dialogue)
    {
        if(dialogue == null)
        {
           EndDialogue();
        }
        else
        {
            ClearChoices();
            StartDialogue(dialogue);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueIndex = 0;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
