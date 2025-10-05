using UnityEngine;

public interface IDamagable 
{
    public void TakeDamage(float damageAmount, Vector2 attackDirection);

    public bool HasTakenDamage { get; set; }
}
