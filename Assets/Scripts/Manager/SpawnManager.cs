using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the spawning of asteroids and stars in the game.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static SpawnManager Instance { get; private set; }

    // Array of asteroid prefabs to spawn
    [SerializeField] private GameObject[] asteroidObjects;

    // Star prefab to spawn
    [SerializeField] private GameObject star;
    
    private float minSpawnDelay = 2f;
     private float maxSpawnDelay = 5f;
     private float spawnRateIncrease = 0.5f;
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnAsteroids()); 
        StartCoroutine(SpawnStarCoroutine());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (GameManager.Instance.IsGamePlaying())
        {
            int checkStar = PlayerCollusion.Instance.GetStarCollectCount(); // Check the number of stars collected

            // Calculate the spawn rate based on the number of stars
            float adjustedSpawnDelay = Mathf.Max(minSpawnDelay, maxSpawnDelay - (checkStar / 5) * spawnRateIncrease); // Decrease wait time

            int randomCount = UnityEngine.Random.Range(1, 4 + (checkStar / 5)); // Random number of asteroids to spawn
            for (int i = 0; i < randomCount; i++)
            {
                SpawnAsteroid(); // Spawn an asteroid
            }

            yield return new WaitForSeconds(adjustedSpawnDelay); // Wait before next spawn
        }
    }

    private IEnumerator SpawnStarCoroutine()
    {
        while (GameManager.Instance.IsGamePlaying())
        {
            SpawnStar(); // Spawn a star
            float waitTime = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay); // Random wait time
            yield return new WaitForSeconds(waitTime); // Wait before next spawn
        }
    }

    private void SpawnAsteroid()
    {
        float xPos = 24f; // Horizontal boundary for asteroids
        float yPos = 13f; // Vertical spawn position for asteroids
        Vector3 asteroidsSpawnPos = new Vector3(UnityEngine.Random.Range(-xPos, xPos), yPos, 0); // Random x position
        int randomIndex = UnityEngine.Random.Range(0, asteroidObjects.Length); // Randomly select an asteroid prefab
        Instantiate(asteroidObjects[randomIndex], asteroidsSpawnPos, Quaternion.identity); // Spawn the asteroid
    }

    private void SpawnStar()
    {
        float duration = 8f; // Duration before the star is destroyed
        float xPos = 20f; // Horizontal boundary for stars
        float yPos = 8f; // Vertical boundary for stars
        Vector3 starSpawnPos = new Vector3(UnityEngine.Random.Range(-xPos, xPos), UnityEngine.Random.Range(-yPos, yPos), 0); // Random position
        GameObject newStar = Instantiate(star, starSpawnPos, Quaternion.identity); // Spawn the star
        StartCoroutine(DestroyStarAfterTime(newStar, duration)); // Start coroutine to destroy star after duration
    }

    private IEnumerator DestroyStarAfterTime(GameObject star, float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        Destroy(star); // Destroy the star GameObject
    }
}