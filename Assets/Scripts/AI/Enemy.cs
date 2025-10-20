using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float playerDetectionRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float cooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState;

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

        if(enemyState != EnemyState.KnockBack)
        {
            CheckForPlayer();

            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void Chase()
    {
        if ((player.position.x > transform.position.x && facingDirection == -1) || (player.position.x < transform.position.x && facingDirection == 1))
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void FaceAttackDirection(Vector2 attackDirection)
    {
        if (attackDirection.x > 0 && facingDirection == -1)
        {
            Flip();
        }
        else if (attackDirection.x < 0 && facingDirection == 1)
        {
            Flip();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectionRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= attackRange && cooldownTimer <= 0)
            {
                cooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else if(player != null && Vector2.Distance(transform.position, player.position) < playerDetectionRange)
        {
            ChangeState(EnemyState.Chasing);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    public void ChangeState(EnemyState newState)
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

    public void OnDamagedByPlayer(Vector2 attackDirection)
    {
        FaceAttackDirection(attackDirection);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
            }
        }  
        ChangeState(EnemyState.Chasing);
    }

    public void Die ()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;  
        anim.SetTrigger("Die");

        Destroy(gameObject, 1f);
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
    KnockBack,
}
