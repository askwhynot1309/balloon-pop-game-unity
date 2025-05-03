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
    public TextMeshProUGUI highscoreText;

    private int score = 0;

    public void Start()
    {
        SoundManager.Instance.PlayMusic();
        StartCoroutine(GameAPI.Instance.GetHighScore(
        score =>
        {
            Debug.Log("Fetched high score: " + score);
            highscoreText.text = "Highscore: " + score.ToString();
        },
        error =>
        {
            Debug.LogError("Failed to fetch high score: " + error);
        }));
    }

    public void Awake()
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
        StartCoroutine(GameAPI.Instance.PostPlayHistory(score,
                    onSuccess: () => {
                        Debug.Log("Score posted successfully.");
                    },
                    onError: (error) => {
                        Debug.LogError($"Failed to post score: {error}");
                    }));
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
