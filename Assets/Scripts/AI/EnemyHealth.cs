using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private ParticleSystem damageParticles;
    private float health;

    private DamageFlash _damageFlash;
    private ParticleSystem damageParticlesInstance;

    private Enemy enemy;

    public bool HasTakenDamage { get; set; }

    void Start()
    {
        health = maxHealth;

        _damageFlash = GetComponent<DamageFlash>();
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damage, Vector2 attackDirection)
    {
        HasTakenDamage = true;
    
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        SpawnDamageParticles(attackDirection);
        // to do and sound effect


        if (health <= 0)
        {
            enemy.Die();
        }

         //damage flash effect
         _damageFlash.CallDamageFlash();
    }

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);


        damageParticlesInstance = Instantiate(damageParticles, transform.position, spawnRotation);
    }

}
