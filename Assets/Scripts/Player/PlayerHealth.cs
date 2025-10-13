using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public HealthDisplay health;

    void Start()
    {
        currentHealth = maxHealth;
        health.UpdateHearts(currentHealth, maxHealth);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        health.UpdateHearts(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
