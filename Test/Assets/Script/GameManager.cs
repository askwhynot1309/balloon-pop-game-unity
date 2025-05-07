using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AstraInputController inputController;
    public GameObject restartButton;

    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    private bool isGameOver = false;
    public TextMeshProUGUI highscoreText;

    private int score = 0;

    private FootDetector restartDetector;

    public void Start()
    {
        SoundManager.Instance.PlayMusic();
        gameOverScreen.SetActive(false);
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

        if (inputController == null)
        {
            inputController = FindFirstObjectByType<AstraInputController>();
        }

        if (inputController != null)
        {
            inputController.OnClickEvent.AddListener(HandleFootClick);
        }

        restartDetector = restartButton.GetComponent<FootDetector>();
    }

    void HandleFootClick()
    {
        if (isGameOver)
        {
            if (restartDetector != null && restartDetector.IsFootOver && !restartDetector.hasClicked)
            {
                restartDetector.hasClicked = true;
                RestartGame();
            }
            return;
        }
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
                    onSuccess: () =>
                    {
                        Debug.Log("Score posted successfully.");
                    },
                    onError: (error) =>
                    {
                        Debug.LogError($"Failed to post score: {error}");
                    }));
        gameOverScreen.SetActive(true);
    }

    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        scoreText.text = "Score: 0";
        gameOverScreen.SetActive(false);

        if (restartDetector != null)
        {
            restartDetector.hasClicked = false;
        }

        BalloonSpawner spawner = FindFirstObjectByType<BalloonSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
            spawner.ClearAllBalloons();
            spawner.StartSpawning();
        }
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

}
