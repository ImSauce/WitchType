using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private int facingDirection = -1;
    public EnemyState enemyState;

    public float attackRange = 2f;

    Rigidbody2D rb;
    private Transform player;
    

    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }
    
    void Update()
    {
        if (isDead) return;

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if(enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Chase()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            ChangeState(EnemyState.Attacking);
        }

        else if (player.position.x > transform.position.x && facingDirection == -1 || player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector3 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag ("Player"))
        {
            if(player == null)
            {
               player = collision.transform;
            }
            ChangeState(EnemyState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        if(enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false); 
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

    public void Die ()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;  
        anim.SetTrigger("Die");
    }    

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
}
