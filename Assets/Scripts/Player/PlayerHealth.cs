using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);


        /* if (health <= 0)
        {
            Die();
        }*/
    }

    /*void Die()
    {
        GameOverManager gameOver = Object.FindFirstObjectByType<GameOverManager>();
        if (gameOver != null)
        {
            gameOver.ShowGameOver();
        }

        gameObject.SetActive(false);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
}