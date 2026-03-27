using UnityEngine;

public class PassZone : MonoBehaviour
{
    public int scoreAmount = 1;
    bool passed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!passed && collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreAmount);
            passed = true;
        }
    }
}