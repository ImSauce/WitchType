using UnityEngine;
using System.Collections;


public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }
    public void KnockBack(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy.ChangeState(EnemyState.KnockBack);
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
    }


    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy.ChangeState(EnemyState.Idle);
    }
}
