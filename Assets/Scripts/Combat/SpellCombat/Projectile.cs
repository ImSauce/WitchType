using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;
    public float lifetime = 7f;
    public float kbForce = 5f;
    public float knockbackTime = 0.15f;
    public float stunTime = 1f;
    public int damage = 1;
    public float maxDistance = 10f;
    public float shakeduration = 1f;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip launchSound;

    private Rigidbody2D rb;
    private Vector3 spawnPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;

        // ✅ Get the draggable aim position from MobileAim (if it exists)
        Vector3 targetPos = MobileAim.Instance != null
            ? MobileAim.Instance.GetAimWorldPosition()
            : Camera.main.ScreenToWorldPoint(Input.mousePosition); // fallback for PC testing

        targetPos.z = 0f;
        Vector2 direction = (targetPos - transform.position).normalized;

        rb.linearVelocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (sfxSource != null && launchSound != null)
            sfxSource.PlayOneShot(launchSound);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        float distanceTraveled = Vector3.Distance(spawnPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            EnemyKnockback knockback = other.GetComponent<EnemyKnockback>();

            if (enemy != null)
            {
                CinemachineShake.Instance.ShakeOnce(shakeduration);
                enemy.TakeDamage(damage, transform.right);
            }

            if (knockback != null)
                knockback.KnockBack(transform, kbForce, knockbackTime, stunTime);
        }
    }
}
