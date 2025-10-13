using UnityEngine;

public class Heal : MonoBehaviour
{
    public int healAmount = 1;
    public float destroyDelay = 1f; // Match this to your animation length

    private Animator anim;

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
        }

        Destroy(gameObject, destroyDelay);
    }
}
