using System;
using UnityEngine;

/// <summary>
/// Manages player collision events, such as collecting stars and handling player death.
/// </summary>
public class PlayerCollusion : MonoBehaviour
{
    public static PlayerCollusion Instance { get; private set; }

    public event EventHandler OnPlayerDeath; // Triggered when the player dies
    public event EventHandler OnStarCollected; // Triggered when a star is collected

    [SerializeField] private int starCollect = 0; // Counter for collected stars

    private float xPosLimit; // Horizontal movement limit
    private float yPosMinLimit; // Minimum vertical position limit
    private float yPosMaxLimit; // Maximum vertical position limit

    private bool playerIsAlive = true; // Tracks if the player is alive

    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    private void Start()
    {
        starCollect = 0; // Initialize star collection count

        // Calculate position limits based on the camera size
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float aspectRatio = (float)Screen.width / (float)Screen.height;
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * aspectRatio;

            xPosLimit = cameraWidth / 2 - 0.5f; // Adjust for some margin
            yPosMinLimit = -mainCamera.orthographicSize;
            yPosMaxLimit = mainCamera.orthographicSize;
        }
    }

    private void Update()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xPosLimit, xPosLimit);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yPosMinLimit, yPosMaxLimit);
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.InstantiateParticle(GameManager.Instance.playerDeath, this.transform);
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            playerIsAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.InstantiateParticle(GameManager.Instance.CollectStar, collision.transform);
            OnStarCollected?.Invoke(this, EventArgs.Empty);
            starCollect++;
        }
    }

    public int GetStarCollectCount()
    {
        return starCollect;
    }

    public bool IsPlayerAlive()
    {
        return playerIsAlive;
    }
}