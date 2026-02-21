using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20;

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


}
