using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [Header("Enemies")]
    public Enemy[] prefabs;
    public AnimationCurve speed = new AnimationCurve();
    private Vector3 direction = Vector3.right;
    private Vector3 initialPosition;

    [Header("Missiles")]
    public EnemyProjectile missilePrefab;
    public float missileSpawnRate = 1f;
    

    private float minSpawnDelay = 2f;
    private float maxSpawnDelay = 5f;
    private float spawnRateIncrease = 0.5f;
    private void Awake()
    {
        initialPosition = transform.position;
        Instance = this; 
    }


    private void Start()
    {
    }

   


    public void StartSpawning()
    {
               StartCoroutine(SpawnEnemies()); 
    }
    private IEnumerator SpawnEnemies()
    {
        while (GameManager.Instance.IsGamePlaying())
        {
            int checkStar = PlayerCollusion.Instance.GetStarCollectCount(); // Check the number of stars collected

            // Calculate the spawn rate based on the number of stars
            float adjustedSpawnDelay = Mathf.Max(minSpawnDelay, maxSpawnDelay - (checkStar / 5) * spawnRateIncrease); // Decrease wait time

            int randomCount = UnityEngine.Random.Range(1, 4 + (checkStar / 5)); // Random number of asteroids to spawn
            for (int i = 0; i < randomCount; i++)
            {
                SpawnEnemy(); 
            }

            yield return new WaitForSeconds(adjustedSpawnDelay); // Wait before next spawn
        }
    }

    private void SpawnEnemy()
    {

        float xPos = 24f; 
        float yPos = 25f; 
        Vector3 EnemySpawnPos = new Vector3(UnityEngine.Random.Range(-xPos, xPos), yPos, 0); 
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length); 
        Instantiate(prefabs[randomIndex], EnemySpawnPos, Quaternion.identity); 
    }

    


    }



