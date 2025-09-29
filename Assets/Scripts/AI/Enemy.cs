using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody2D rb;
    public Transform target;
    Vector2 moveDirection;

    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!isDead) return;

        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead) return;

        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
       
    }

    public void Die ()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;  
        animator.SetTrigger("Die");
    }    

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
