using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private Vector2 lastMoveDir = Vector2.down;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (TypeCastingUI.TypeCastingMode) return;
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
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isWalking", false);
    }
}
