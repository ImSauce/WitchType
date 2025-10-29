using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.down;

    // Speed boost coroutine reference
    private Coroutine speedBoostCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    // Called by Input System
    public void Move(InputAction.CallbackContext context)
    {
        if (TypeCastingUI.TypeCastingMode) return;
        if (!context.performed && !context.canceled) return;

        movement = context.ReadValue<Vector2>();

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            lastMoveDir = movement;

            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("InputX", lastMoveDir.x);
            animator.SetFloat("InputY", lastMoveDir.y);
        }
    }

    public void StopMovement()
    {
        SetMovement(Vector2.zero);
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isWalking", false);
    }

    // TEMPORARY SPEED BOOST HANDLER
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        // Stop any previous boost
        if (speedBoostCoroutine != null)
            StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        speedBoostCoroutine = null;
    }
}
