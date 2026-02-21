using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;

    public Transform player;
    public float detectionRange = 5f;

    public float shootDelay = 0.1f;
    float timer;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            timer += Time.deltaTime;

            if (timer >= shootDelay)
            {
                Shoot();
                timer = 0f;
            }
        }
    }


    void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    }
}
