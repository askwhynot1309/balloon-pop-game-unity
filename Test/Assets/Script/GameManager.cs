using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    private bool isGameOver = false;

    private int score = 0; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;

        BalloonSpawner spawner = FindFirstObjectByType<BalloonSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
