using System;
using UnityEngine;

/// <summary>
/// Controls the behavior of missiles in the game, including movement and collision detection.
/// </summary>
public class MissileController : MonoBehaviour
{
    // Singleton instance for easy access
    public static MissileController Instance { get; private set; }

    // Event triggered when the missile hits an object
    public event EventHandler OnMissleHit;

    // Speed at which the missile travels
    [SerializeField] private float missleSpeed = 50f;

    /// <summary>
    /// Initializes the MissileController instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Updates the missile's position each frame.
    /// </summary>
    private void Update()
    {
        MissileFowardMove(); // Move the missile forward
        CheckBoundary(); // Check if the missile is out of bounds
    }

    /// <summary>
    /// Moves the missile upward at a constant speed.
    /// </summary>
    private void MissileFowardMove()
    {
        // Move the missile upwards based on its speed
        transform.Translate(Vector3.up * missleSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the missile has moved out of the defined boundaries.
    /// </summary>
    private void CheckBoundary()
    {
        float xPos = 24f; // Horizontal boundary limit
        float yPos = 11f; // Vertical boundary limit

        // Destroy the missile if it goes out of bounds
        if (IsOutOfBounds(xPos, yPos))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Determines if the missile is out of the defined boundaries.
    /// </summary>
    /// <param name="xPos">The horizontal boundary limit.</param>
    /// <param name="yPos">The vertical boundary limit.</param>
    /// <returns>True if the missile is out of bounds; otherwise, false.</returns>
    private bool IsOutOfBounds(float xPos, float yPos)
    {
        return transform.position.x > xPos || transform.position.x < -xPos ||
               transform.position.y > yPos || transform.position.y < -yPos;
    }

    /// <summary>
    /// Handles collision detection with other objects.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the missile collides with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Instantiate explosion particle effect
            GameManager.Instance.InstantiateParticle(GameManager.Instance.explosionParticle, this.transform);
            // Destroy the asteroid on collision
            Destroy(collision.gameObject);
            Destroy(this.gameObject); // Destroy the missile object
            OnMissleHit?.Invoke(this, EventArgs.Empty); // Invoke the event for missile hit
        }
    }
}