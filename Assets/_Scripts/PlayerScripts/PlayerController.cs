using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;  // Maximum number of jumps (2 for double jump)
    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount;  // Track the number of jumps

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;  // Allow full jumps at the start
    }

    void Update()
    {
        // Horizontal movement input
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip player sprite based on movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;  // Decrease the jump count on each jump
        }

        // Set Animator parameters
        HandleAnimations(moveInput);
    }

    private void HandleAnimations(float moveInput)
    {
        // If the player is grounded, handle Idle or Running animations
        if (isGrounded)
        {
            if (moveInput != 0)
                animator.Play("Run_Player");  // Play running animation
            else
                animator.Play("Idle_Player"); // Play idle animation
        }

        // Handle Jump and Fall animations based on vertical velocity
        if (!isGrounded)
        {
            if (rb.velocity.y > 0)
            {
                animator.Play("Jump_Player");  // Play jump animation while moving upwards
            }
            else if (rb.velocity.y < 0)
            {
                animator.Play("Fall_Player");  // Play fall animation while descending
            }
        }
    }

    // Ground check: use collision detection to know if the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = maxJumps;  // Reset jump count when landing
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
