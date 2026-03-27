using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float detectionRange = 10f;
    public float speed = 4f;

    private Transform player;

    void Start()
    {
        // Automatically find the player so you don't have to assign them in the inspector for every enemy
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check how far away the player is
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Calculates the exact angle directed specifically from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Moves the enemy directly toward the player's current position (Chaser/Homing mode)
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
