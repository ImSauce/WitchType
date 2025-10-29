using UnityEngine;
using System.Collections;

public class CollectSpeedBoost : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speedMultiplier = 1.5f; // How much faster the player moves
    public float boostDuration = 5f;     // Duration of the speed boost
    public float destroyDelay = 1f;      // Delay before destroying the object

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip pickupSound;

    [Header("UI")]
    [SerializeField] private GameObject _speedPanel; 
    [SerializeField] private float _panelDuration = 0.25f; 

    [Header("Visuals")]
    [SerializeField] private GameObject visualChild; // Sprite/animation
    [SerializeField] private GameObject lightChild;  // Light2D child

    private Animator anim;

    private void Start()
    {
        if (visualChild != null)
            anim = visualChild.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply speed boost
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(speedMultiplier, boostDuration);
            }

            // Play visual/audio effects
            if (anim != null) anim.Play("SpeedEffect");
            if (sfxSource != null && pickupSound != null) sfxSource.PlayOneShot(pickupSound);
            if (_speedPanel != null) StartCoroutine(ShowPanel());

            // Disable collider so it can't trigger again
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            // Hide visuals
            if (visualChild != null)
            {
                SpriteRenderer sr = visualChild.GetComponent<SpriteRenderer>();
                if (sr != null) sr.enabled = false;
            }
            if (lightChild != null)
                lightChild.SetActive(false);

            // Destroy parent after effects
            StartCoroutine(DestroyAfterEffects());
        }
    }

    private IEnumerator ShowPanel()
    {
        _speedPanel.SetActive(true);
        yield return new WaitForSeconds(_panelDuration);
        _speedPanel.SetActive(false);
    }

    private IEnumerator DestroyAfterEffects()
    {
        float waitTime = Mathf.Max(destroyDelay, _panelDuration);
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
