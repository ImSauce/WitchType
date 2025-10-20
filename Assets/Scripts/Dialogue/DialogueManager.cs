using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    public bool isDialogueActive;

    [Header("UI Elements")]
    public CanvasGroup canvasGroup;
    public Image potrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;
    public Button nextButton;

    private PlayerMovement player;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private float lastDialogueTime;
    private float dialogueCooldown = .1f;

    private void Awake()
    {

        player = Object.FindAnyObjectByType<PlayerMovement>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(false);
            nextButton.onClick.AddListener(AdvanceDialogue);
        }
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        if(Time.unscaledTime - lastDialogueTime < dialogueCooldown)
        {
            return;
        }
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

    public bool CanStartDialogue()
    {
        return Time.unscaledTime - lastDialogueTime >= dialogueCooldown;
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        GameManager.Instance.DialogueHistoryTracker.RecordNPC(line.speaker);
        potrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (player != null)
        {
            player.StopMovement();
            player.enabled = false;
        }


        if (nextButton != null)
            nextButton.gameObject.SetActive(true);

        dialogueIndex++;
    }

    private void ShowChoices()
    {
        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

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
        EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
    }

    private void ChooseOption(DialogueSO dialogue)
    {
        if (dialogue == null)
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

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (player != null)
            player.enabled = true;

        lastDialogueTime = Time.unscaledTime;
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
