using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;

    public Transform player;
    public float detectionRange = 5f;

    public float shootDelay = 0.1f;
    float timer;

    public AudioClip shootSound;
    AudioSource audioSource;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            timer += Time.deltaTime;

            // Make enemies shoot faster as the score goes up!
            // Decrease the delay based on score, but cap it so it doesn't shoot too fast (minimum 0.02f delay)
            float currentShootDelay = shootDelay;
            if (ScoreManager.instance != null)
            {
                currentShootDelay = Mathf.Max(0.02f, shootDelay - (ScoreManager.instance.score * 0.015f));
            }

            if (timer >= currentShootDelay)
            {
                Shoot();
                timer = 0f;
            }
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
