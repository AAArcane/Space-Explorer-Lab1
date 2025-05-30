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

    // Minimum and maximum spawn delays for asteroids and stars
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 5f;

    /// <summary>
    /// Initializes the SpawnManager instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Starts the spawning process for asteroids and stars.
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(SpawnAsteroids()); // Start asteroid spawning coroutine
        StartCoroutine(SpawnStarCoroutine()); // Start star spawning coroutine
    }

    /// <summary>
    /// Coroutine that spawns asteroids at random intervals.
    /// </summary>
    /// <returns>Waits for the specified delay before spawning more asteroids.</returns>
    private IEnumerator SpawnAsteroids()
    {
        while (GameManager.Instance.IsGamePlaying())
        {
            int randomCount = UnityEngine.Random.Range(1, 4); // Random number of asteroids to spawn
            for (int i = 0; i < randomCount; i++)
            {
                SpawnAsteroid(); // Spawn an asteroid
            }
            float waitTime = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay); // Random wait time
            yield return new WaitForSeconds(waitTime); // Wait before next spawn
        }
    }

    /// <summary>
    /// Coroutine that spawns stars at random intervals.
    /// </summary>
    /// <returns>Waits for the specified delay before spawning more stars.</returns>
    private IEnumerator SpawnStarCoroutine()
    {
        while (GameManager.Instance.IsGamePlaying())
        {
            SpawnStar(); // Spawn a star
            float waitTime = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay); // Random wait time
            yield return new WaitForSeconds(waitTime); // Wait before next spawn
        }
    }

    /// <summary>
    /// Spawns an asteroid at a random position within specified boundaries.
    /// </summary>
    private void SpawnAsteroid()
    {
        float xPos = 24f; // Horizontal boundary for asteroids
        float yPos = 13f; // Vertical spawn position for asteroids
        Vector3 asteroidsSpawnPos = new Vector3(UnityEngine.Random.Range(-xPos, xPos), yPos, 0); // Random x position
        int randomIndex = UnityEngine.Random.Range(0, asteroidObjects.Length); // Randomly select an asteroid prefab
        Instantiate(asteroidObjects[randomIndex], asteroidsSpawnPos, Quaternion.identity); // Spawn the asteroid
    }

    /// <summary>
    /// Spawns a star at a random position within specified boundaries.
    /// </summary>
    private void SpawnStar()
    {
        float duration = 8f; // Duration before the star is destroyed
        float xPos = 20f; // Horizontal boundary for stars
        float yPos = 8f; // Vertical boundary for stars
        Vector3 starSpawnPos = new Vector3(UnityEngine.Random.Range(-xPos, xPos), UnityEngine.Random.Range(-yPos, yPos), 0); // Random position
        GameObject newStar = Instantiate(star, starSpawnPos, Quaternion.identity); // Spawn the star
        StartCoroutine(DestroyStarAfterTime(newStar, duration)); // Start coroutine to destroy star after duration
    }

    /// <summary>
    /// Coroutine to destroy the star after a specified duration.
    /// </summary>
    /// <param name="star">The star GameObject to destroy.</param>
    /// <param name="duration">Time to wait before destroying the star.</param>
    /// <returns>Waits for the specified duration before destroying.</returns>
    private IEnumerator DestroyStarAfterTime(GameObject star, float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        Destroy(star); // Destroy the star GameObject
    }
}