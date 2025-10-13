using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image[] hearts;

    public void UpdateHearts(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            Animator anim = hearts[i].GetComponent<Animator>();

            // Check if heart should be visible
            if (i < currentHealth)
            {
                hearts[i].enabled = true;

                // Play animation when heart updates
                if (anim != null && anim.HasState(0, Animator.StringToHash("HeartUpdate")))
                {
                    anim.Play("HeartUpdate", -1, 0f);
                }
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
