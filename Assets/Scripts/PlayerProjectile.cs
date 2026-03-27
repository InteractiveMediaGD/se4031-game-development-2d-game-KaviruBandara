using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Camera.main != null)
        {
            // Calculate the actual distance from the camera to the projectile's depth plane
            float zDist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
            
            // Find the exact world X positions of the left and right edges of the screen at this depth
            float rightEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, zDist)).x;
            float leftEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDist)).x;
            
            // Destroy if it travels past the screen edges + a small 2 unit buffer
            if (transform.position.x > rightEdgeX + 2f || transform.position.x < leftEdgeX - 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ScoreManager.instance.AddScore(1);

            EnemyCollision enemy = collision.GetComponent<EnemyCollision>();
            if (enemy != null)
            {
                enemy.PlayDestroySound();
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Blocker"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(float facing)
    {
        direction = new Vector2(facing, 0);
    }
}
