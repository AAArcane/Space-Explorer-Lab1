using System;
using UnityEngine;

public class PlayerCollusion : MonoBehaviour
{
    public static PlayerCollusion Instance { get; private set; }

    public event EventHandler OnPlayerDeath;
    public event EventHandler OnStarCollected;

    [SerializeField] private int starCollect ;
    private bool playerIsAlive = true;
    private Camera mainCamera;
    private Transform playerRoot;
    private int damage;

    // Custom Y-axis boundaries
    private const float Y_MIN = -6.2f;
    private const float Y_MAX = 21f;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        playerRoot = transform.parent;

        if (playerRoot == null)
        {
            playerRoot = transform;
            Debug.LogWarning("Player_Visual has no parent! Using self as root.");
        }
    }

    private void Update()
    {
        ClampPlayerPosition();
    }

    private void ClampPlayerPosition()
    {
        if (mainCamera == null) return;

        Vector3 position = playerRoot.position;

        // Calculate X boundaries with padding (0.5 units)
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.position.z));

        // Apply clamping
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        position.y = Mathf.Clamp(position.y, Y_MIN, Y_MAX);

        playerRoot.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (!playerIsAlive) return; 
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            damage = 20;
            PlayerController.Instance.TakeDamage(damage);
            HandlePlayerDeath();
            Destroy(collision.gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            HandleStarCollection(collision.gameObject);
        }
    }

    private void HandlePlayerDeath()
    {
        int health = PlayerController.Instance.GetCurrentHealth();
        if (health <= 0)
        {
            playerIsAlive = false;


            GameManager.Instance.InstantiateParticle(
                GameManager.Instance.playerDeath,
                playerRoot.transform
            );
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            playerRoot.gameObject.SetActive(false);
        }
        
    }

    private void HandleStarCollection(GameObject star)
    {
        starCollect = 30;
        ScoreUI.Instance.AddScore(starCollect);
        Destroy(star);

        GameManager.Instance.InstantiateParticle(
            GameManager.Instance.CollectStar,
            star.transform
        );
        OnStarCollected?.Invoke(this, EventArgs.Empty);
    }

    public int GetStarCollectCount() => starCollect;
    public bool IsPlayerAlive() => playerIsAlive;

    
}