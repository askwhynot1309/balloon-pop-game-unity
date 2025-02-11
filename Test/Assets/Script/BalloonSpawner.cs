using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject redBalloon;
    public GameObject blueBalloon;
    public GameObject bomb;
    public float spawnRateRed = 3f;
    public float spawnRateBlue = 5f;
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);
    public Vector2 spawnRangeY = new Vector2(-8f, 8f);
    public float balloonLifetime = 3f;
    public float bombSpawnChance = 0.5f;

    public bool stopSpawning = false;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRedBalloon), 1f, spawnRateRed);
        InvokeRepeating(nameof(SpawnBlueBalloon), 1f, spawnRateBlue);

    }

    void SpawnRedBalloon()
    {
        if (stopSpawning) return;

        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject prefabToSpawn;

        // Random bomb
        if (Random.value < bombSpawnChance)
        {
            prefabToSpawn = bomb;
        }
        else
        {
            prefabToSpawn = redBalloon;
        }

        GameObject red = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        Destroy(red, balloonLifetime);
    }

    void SpawnBlueBalloon()
    {
        if (stopSpawning) return;

        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject blue = Instantiate(blueBalloon, spawnPosition, Quaternion.identity);

        Destroy(blue, balloonLifetime);
    }
    public void StopSpawning()
    {
        stopSpawning = true;
    }
}
