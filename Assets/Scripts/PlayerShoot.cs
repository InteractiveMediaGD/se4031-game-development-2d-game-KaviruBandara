using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;

    public AudioClip shootSound;
    
    [Header("Combat Settings")]
    public float fireRate = 0.4f; // Delay in seconds between each shot
    private float nextFireTime = 0f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource missing on Player!");
        }
    }

    void Update()
    {
        // Only allow shooting if the current time is greater than our calculated next available fire time
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Reset the timer for the next shot
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        PlayerProjectile projectile = proj.GetComponent<PlayerProjectile>();

        if (projectile != null)
        {
            float facing = transform.localScale.x;
            projectile.SetDirection(facing);
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}