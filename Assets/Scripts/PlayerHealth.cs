using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public Image healthFill;
    public Gradient healthGradient;
    public AudioClip damageSound;
    AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        healthFill.color = healthGradient.Evaluate(1f);
        audioSource = GetComponent<AudioSource>();
    }

    void Update() //  TEST ONLY
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        CameraShake.instance.Shake(0.2f, 0.1f);

        currentHealth -= damage;

        healthBar.value = currentHealth;
        UpdateHealthColor();

        audioSource.PlayOneShot(damageSound);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game Over");

        Time.timeScale = 0f; // stops game
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        GameManager.instance.GameOver();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealthColor();
    }

    void UpdateHealthColor()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthFill.color = healthGradient.Evaluate(healthPercent);
    }
}
