using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float health;

    private DamageFlash _damageFlash;
    private bool isDead = false;

    private Enemy enemy;


    void Start()
    {
        health = maxHealth;

        _damageFlash = GetComponent<DamageFlash>();
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(int damage)
    {
    
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        // damage particles
        // to do and sound effect


         if (health <= 0)
        {
            enemy.Die();
        }

         //damage flash effect
         _damageFlash.CallDamageFlash();
    }

}
