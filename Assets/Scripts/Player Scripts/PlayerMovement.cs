using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Use SerializeField to allow editing in inspector 
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

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
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
        }

        movement = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
    }
}