using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack Instance { get; private set; }

    private bool isReady;
    private float fireAgain = 4f; // Set initial cooldown time

    [SerializeField] private Transform missilePosition;
    [SerializeField] private GameObject missile;

    private void Awake()
    {
        Instance = this;
        isReady = false;
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            isReady = true; // Set ready if player is found
        }
        else
        {
            Debug.LogWarning("Player not found! Projectile will not move.");
        }
    }

    private void SpawnMissile()
    {
        if (!gameObject.activeInHierarchy) return;
        if (!GameManager.Instance.IsGamePlaying()) return;

        fireAgain -= Time.deltaTime;

        if (fireAgain <= 0)
        {
            GameObject spawnMissile = Instantiate(missile, missilePosition.position, Quaternion.identity);
            EnemyProjectile projectile = spawnMissile.GetComponent<EnemyProjectile>();

            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                projectile.Fire(player.transform.position); // Aim the missile at the player's position
            }

            fireAgain = 5f; // Reset the fire cooldown
        }
    }

    private void Update()
    {
        if (isReady)
        {
            // Destroy when outside screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (transform.position.x < min.x || transform.position.x > max.x)
            {
                Destroy(gameObject);
            }

            SpawnMissile(); // Check for missile spawning
        }
    }
}