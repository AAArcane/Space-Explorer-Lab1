using System;
using UnityEngine;

public class PlayerCollusion : MonoBehaviour
{
    public static PlayerCollusion Instance { get; private set; }

    public event EventHandler OnPlayerDeath;
    public event EventHandler OnStarCollected;

    [SerializeField] private int starCollect = 0;
    private bool playerIsAlive = true;
    private Camera mainCamera;
    private Transform playerRoot;

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
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            HandlePlayerDeath();
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
        playerIsAlive = false;
       

        GameManager.Instance.InstantiateParticle(
            GameManager.Instance.playerDeath,
            playerRoot.transform // Use root position
        );

        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        playerRoot.gameObject.SetActive(false);
    }

    private void HandleStarCollection(GameObject star)
    {
        
        Destroy(star);
        starCollect++;

        GameManager.Instance.InstantiateParticle(
            GameManager.Instance.CollectStar,
            star.transform
        );

        OnStarCollected?.Invoke(this, EventArgs.Empty);
    }

    public int GetStarCollectCount() => starCollect;
    public bool IsPlayerAlive() => playerIsAlive;

    // Gizmos for visualization
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || mainCamera == null) return;

        // Draw custom Y boundaries
        Gizmos.color = Color.cyan;
        Vector3 left = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        Vector3 right = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.position.z));

        // Draw min line
        Gizmos.DrawLine(
            new Vector3(left.x, Y_MIN, 0),
            new Vector3(right.x, Y_MIN, 0)
        );

        // Draw max line
        Gizmos.DrawLine(
            new Vector3(left.x, Y_MAX, 0),
            new Vector3(right.x, Y_MAX, 0)
        );
    }
}