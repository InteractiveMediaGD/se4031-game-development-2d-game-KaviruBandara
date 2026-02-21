using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator animator;

    float moveInput;
    bool isGrounded;

    float blinkTimer;
    float nextBlinkTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        SetNextBlink();
    }

    void Update()
    {
        // Horizontal movement
        moveInput = Input.GetAxisRaw("Horizontal");

        // Stop blinking if player starts moving
        if (moveInput != 0)
        {
            blinkTimer = 0f;
            SetNextBlink();
        }

        // Jump using SPACE
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Flip character
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        // Animator updates
        animator.SetBool("isRunning", moveInput != 0 && isGrounded && Mathf.Abs(rb.velocity.y) < 0.01f);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);

        // RANDOM BLINK
        blinkTimer += Time.deltaTime;

        if (blinkTimer >= nextBlinkTime)
        {
            if (IsIdle() && !IsBlinking())
            {
                animator.SetTrigger("Blink");
                SetNextBlink();
            }
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Blink timing
    void SetNextBlink()
    {
        blinkTimer = 0f;
        nextBlinkTime = Random.Range(1f, 2f);
    }

    bool IsIdle()
    {
        return moveInput == 0 && isGrounded;
    }

    bool IsBlinking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("New_Blink");
    }
}
