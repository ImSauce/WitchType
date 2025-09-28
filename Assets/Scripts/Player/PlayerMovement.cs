using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Use SerializeField to allow editing in inspector 
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
        movement = context.ReadValue<Vector2>();

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            // update last direction when moving
            lastMoveDir = movement;

            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }
        else
        {
            animator.SetBool("isWalking", false);

            // keep facing the last direction when idle
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
