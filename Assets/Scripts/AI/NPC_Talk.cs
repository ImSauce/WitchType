using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSO dialogue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

   private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        anim.Play("Idle");
        interactAnim.Play("Open");
    }

   private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            {
                DialogueManager.Instance.StartDialogue(dialogue);   
            }
        }
    }
}
