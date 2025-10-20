using UnityEngine;

public class Heal : MonoBehaviour
{
    public int healAmount = 1;
    public float destroyDelay = 1f; // Match this to your animation length

    private Animator anim;


    [Header("SFX")]
    public AudioSource sfxSource;        
    public AudioClip HealSound;       


    private void Start()
    {
        anim = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(healAmount);
            }
        }

        if (anim != null)
        {
            anim.Play("HealEffect");

            if (sfxSource != null && HealSound != null)
                sfxSource.PlayOneShot(HealSound);
            
        }

        Destroy(gameObject, destroyDelay);
    }
}
