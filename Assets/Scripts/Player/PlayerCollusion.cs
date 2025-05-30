using System;
using UnityEngine;

/// <summary>
/// Manages player collision events, such as collecting stars and handling player death.
/// </summary>
public class PlayerCollusion : MonoBehaviour
{
    // Singleton instance for easy access
    public static PlayerCollusion Instance { get; private set; }

    // Events for player actions
    public event EventHandler OnPlayerDeath; // Triggered when the player dies
    public event EventHandler OnStarCollected; // Triggered when a star is collected

    private int starCollect = 0; // Counter for collected stars

    // Position limits for player movement
    private float xPosLimit = 20.5f; // Horizontal movement limit
    private float yPosMinLimit = -9f; // Minimum vertical position limit
    private float yPosMaxLimit = 8.5f; // Maximum vertical position limit

    private bool playerIsAlive = true; // Tracks if the player is alive

    /// <summary>
    /// Initializes the PlayerCollusion instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Initializes the star collection count.
    /// </summary>
    private void Start()
    {
        starCollect = 0; // Initialize star collection count
    }

    /// <summary>
    /// Updates the player's position to keep it within defined limits.
    /// </summary>
    private void Update()
    {
        // Clamp the player's position within the defined limits
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xPosLimit, xPosLimit); // Clamp horizontal position
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yPosMinLimit, yPosMaxLimit); // Clamp vertical position
        transform.position = clampedPosition; // Update the player's position
    }

    /// <summary>
    /// Handles collisions with other objects.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject); // Destroy the player object
            GameManager.Instance.InstantiateParticle(GameManager.Instance.playerDeath, this.transform); // Instantiate death particle effect
            OnPlayerDeath?.Invoke(this, EventArgs.Empty); // Trigger player death event
            playerIsAlive = false; // Set player alive status to false
        }
    }

    /// <summary>
    /// Handles trigger collisions with collectible items.
    /// </summary>
    /// <param name="collision">The collider data.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with a star collectible
        if (collision.gameObject.CompareTag("Point"))
        {
            // Add logic to handle power-up collection
            Destroy(collision.gameObject); // Destroy the collectible item after collection
            GameManager.Instance.InstantiateParticle(GameManager.Instance.CollectStar, collision.transform); // Instantiate collection particle effect
            OnStarCollected?.Invoke(this, EventArgs.Empty); // Trigger star collected event
            starCollect++; // Increment star collection count
        }
    }

    /// <summary>
    /// Gets the current count of collected stars.
    /// </summary>
    /// <returns>The number of collected stars.</returns>
    public int GetStarCollectCount()
    {
        return starCollect; // Return the star collection count
    }

    /// <summary>
    /// Checks if the player is alive.
    /// </summary>
    /// <returns>True if the player is alive; otherwise, false.</returns>
    public bool IsPlayerAlive()
    {
        return playerIsAlive; // Return the player's alive status
    }
}