using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int damage = 10;
    public AudioClip destroySound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            PlayDestroySound();
            Destroy(gameObject);
        }
    }

    public void PlayDestroySound()
    {
        if (destroySound != null)
        {
            Play2DSound(destroySound);
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

    void Update()
    {
        // Destroy enemy if player goes past them (matches HealthPack logic for rubric requirement)
        if (Camera.main != null && transform.position.x < Camera.main.transform.position.x - 15f)
        {
            Destroy(gameObject);
        }
    }
}