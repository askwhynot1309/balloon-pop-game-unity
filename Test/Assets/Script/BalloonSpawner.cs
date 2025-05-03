using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject redBalloon;
    public GameObject blueBalloon;
    public GameObject bomb;

    public float spawnRateRed = 3f;
    public float spawnRateBlue = 5f;
    public float spawnRateBomb = 7f;
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);
    public Vector2 spawnRangeY = new Vector2(-8f, 8f);
    public float balloonLifetime = 3f;
    public bool stopSpawning = false;

    public AstraInputController inputController;

    private bool hasStartedSpawning = false;

    private void Start()
    {
        bomb = Resources.Load<GameObject>("Bomb");
        if (bomb == null)
        {
            Debug.LogError("Bomb prefab is missing in Resources folder!");
            return;
        }

        if (inputController != null)
        {
            inputController.onDetectBody += OnBodyDetected;
        }
        else
        {
            Debug.LogError("AstraInputController chưa được gán!");
        }
    }

    private void OnDestroy()
    {
        if (inputController != null)
        {
            inputController.onDetectBody -= OnBodyDetected;
        }
    }

    private void OnBodyDetected(bool isDetected, Vector3 _)
    {
        if (isDetected && !hasStartedSpawning)
        {
            hasStartedSpawning = true;

            InvokeRepeating(nameof(SpawnRedBalloon), 1f, spawnRateRed);
            InvokeRepeating(nameof(SpawnBlueBalloon), 1f, spawnRateBlue);
            InvokeRepeating(nameof(SpawnBomb), 1f, spawnRateBomb);
        }
    }

    void SpawnRedBalloon()
    {
        if (stopSpawning) return;
        SpawnBalloon(redBalloon);
    }

    void SpawnBlueBalloon()
    {
        if (stopSpawning) return;
        SpawnBalloon(blueBalloon);
    }

    void SpawnBomb()
    {
        if (stopSpawning) return;
        SpawnBalloon(bomb);
    }

    void SpawnBalloon(GameObject prefab)
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Destroy(obj, balloonLifetime);
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }
}
