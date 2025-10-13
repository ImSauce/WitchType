using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weoponRange;
    public LayerMask playerLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>()?.ChangeHealth(-damage);
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weoponRange, playerLayer);

        if(hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>()?.ChangeHealth(-damage);
        }
    }
}
