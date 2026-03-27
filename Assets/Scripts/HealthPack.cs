using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20;
    public AudioClip healSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.currentHealth += healAmount;

                if (health.currentHealth > health.maxHealth)
                {
                    health.currentHealth = health.maxHealth;
                }

                health.healthBar.value = health.currentHealth;
            }

            if (healSound != null)
            {
                Play2DSound(healSound);
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Camera.main == null) return;

        if (transform.position.x < Camera.main.transform.position.x - 10f)
        {
            Destroy(gameObject);
        }
    }

    void Play2DSound(AudioClip clip)
    {
        GameObject temp = new GameObject("TempAudio");
        AudioSource source = temp.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = 1.5f;
        source.spatialBlend = 0f; //  Makes it 2D
        source.Play();

        Destroy(temp, clip.length);
    }
}