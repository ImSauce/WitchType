using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public HealthDisplay health;

    private DamageFlashPlayer _damageFlash;

    void Start()
    {
        StartCoroutine(InitializeHealthUI());
        _damageFlash = GetComponent<DamageFlashPlayer>();
    }

    private IEnumerator InitializeHealthUI()
    {
        yield return new WaitUntil(() => gameObject.scene.isLoaded);

        currentHealth = maxHealth;

        yield return null;

        if (health != null)
        {
            health.UpdateHearts(currentHealth, maxHealth);
        }
            
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (amount < 0 && _damageFlash != null)
        {
            _damageFlash.CallDamageFlash();
        }

        if (health != null)
            health.UpdateHearts(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }


    }
}
