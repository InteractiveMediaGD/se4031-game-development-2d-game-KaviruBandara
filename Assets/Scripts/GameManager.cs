using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public GameObject gameWonPanel;

    void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameWon()
    {
        if (gameWonPanel != null)
        {
            gameWonPanel.SetActive(true);
        }

        // Stop player input but allow physics to let them land safely
        PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        if (pm != null)
        {
            pm.canMove = false;
        }

        PlayerShoot ps = FindObjectOfType<PlayerShoot>();
        if (ps != null) ps.enabled = false;

        StartCoroutine(WinDelay(3f));
    }

    System.Collections.IEnumerator WinDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GoToMainMenu();
    }
}