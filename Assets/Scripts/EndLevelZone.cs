using UnityEngine;

public class EndLevelZone : MonoBehaviour
{
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the zone, and make sure we only trigger the win once
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;

            // Notify the GameManager that the player has reached the end
            if (GameManager.instance != null)
            {
                GameManager.instance.GameWon();
            }
            else
            {
                Debug.LogWarning("GameManager instance not found! The player won, but no UI can be shown.");
            }
        }
    }
}
