using UnityEngine;

public class HorizontalBlade : MonoBehaviour
{
    [Header("Behavior Settings")]
    public float detectionRange = 12f;
    public float speed = 8f;
    
    [Header("Visual Settings")]
    public float rotationSpeed = 600f; // Extremely fast spinning value

    [Header("Audio Settings")]
    public AudioSource spinSound; // The sound it plays when it starts rolling

    private Transform player;
    private bool isTriggered = false;
    private Vector3 moveDirection;

    void Start()
    {
        // Automatically locate the player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (!isTriggered)
        {
            // Wait until the player enters the detection range
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                isTriggered = true; // The player crossed the threshold! Let's roll!

                // Play the scary rolling/spinning sound!
                if (spinSound != null)
                {
                    spinSound.Play();
                }
                
                // Determine whether to roll left or right based on where the player is standing
                if (player.position.x < transform.position.x)
                {
                    moveDirection = Vector3.left;
                    rotationSpeed = Mathf.Abs(rotationSpeed); // Spin counter-clockwise when rolling left
                }
                else
                {
                    moveDirection = Vector3.right;
                    rotationSpeed = -Mathf.Abs(rotationSpeed); // Spin clockwise when rolling right
                }
            }
        }
        else
        {
            // 1. Move constantly in a straight horizontal line
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

            // 2. Continually rotate the sprite visually creating a rolling/buzzsaw effect
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // 3. Clean up the blade if it travels too far off-screen so you don't leak memory
            if (Camera.main != null)
            {
                float zDist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
                float rightEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, zDist)).x;
                float leftEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDist)).x;

                if (transform.position.x > rightEdgeX + 5f || transform.position.x < leftEdgeX - 5f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Handles the destruction when the blade crashes into ground or blockers
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Blocker"))
        {
            Destroy(gameObject);
        }
    }
    
    // Backup check in case your ground/blockers use Triggers instead of solid colliders
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Blocker"))
        {
            Destroy(gameObject);
        }
    }
}
