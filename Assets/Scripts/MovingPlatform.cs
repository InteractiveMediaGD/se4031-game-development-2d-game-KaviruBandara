using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints")]
    // The two points the platform will move between
    // You can use empty objects for these
    public Transform posA;
    public Transform posB;
    
    [Header("Settings")]
    public float speed = 2f;

    private Vector3 targetPos;
    private Rigidbody2D playerRb;
    private Vector3 lastPos;

    void Start()
    {
        if (posA == null || posB == null)
        {
            Debug.LogWarning("MovingPlatform relies on posA and posB. Please assign them in the inspector.");
            return;
        }

        targetPos = posB.position;
        lastPos = transform.position;
    }

    void FixedUpdate() 
    {
        if (posA == null || posB == null) return;

        // Check if we reached the target position
        if (Vector2.Distance(transform.position, posA.position) < 0.1f)
            targetPos = posB.position;
        else if (Vector2.Distance(transform.position, posB.position) < 0.1f)
            targetPos = posA.position;

        // Move the platform
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

        // Calculate how much we just moved
        Vector3 deltaMovement = transform.position - lastPos;
        
        // If a player is standing on the platform, manually move them along with us
        // This prevents the player from sliding off or squishing scale via transform parent
        if (playerRb != null)
        {
            playerRb.transform.position += deltaMovement;
        }
        
        lastPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = null;
        }
    }
}
