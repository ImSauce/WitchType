using UnityEngine;

public class Switch : MonoBehaviour
{
    public Spike[] controlledSpikes;
    public bool isOn = false;
    public Animator animator;
    private bool playerInRange;
    private AudioManager audioManager;


    private void Start()
    {

        audioManager = UnityEngine.Object.FindFirstObjectByType<AudioManager>();
        if (animator != null)
            animator.SetBool("IsOn", isOn);
    }


    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleSwitch();
            audioManager.PlaySFX("Switch");
        }
    }

    private void ToggleSwitch()
    {
        isOn = !isOn;

        if (animator != null)
            animator.SetBool("IsOn", isOn);

        foreach (var spike in controlledSpikes)
        {
            if (spike != null)
                spike.SetActive(!isOn); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}
