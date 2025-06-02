using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the movement and behavior of asteroids in the game.
/// </summary>
public class AsteroidsMovement : MonoBehaviour
{
    // Speed at which the asteroid moves downwards
    [SerializeField] private float speed = 2.5f;

    // Range for random horizontal movement
    [SerializeField] private float horizontalMovementRange = 1.0f;

    // Speed for horizontal movement, randomized at start
    private float horizontalMovementSpeed;

    /// <summary>
    /// Initializes the asteroid's horizontal movement speed.
    /// </summary>
    private void Start()
    {
        // Randomize the horizontal movement speed within the specified range
        horizontalMovementSpeed = Random.Range(-horizontalMovementRange, horizontalMovementRange);
    }

    /// <summary>
    /// Updates the asteroid's position each frame.
    /// </summary>
    private void Update()
    {
        MoveAsteroid(); // Move the asteroid
        DestroyOutOfBounds(); // Check if the asteroid is out of bounds
    }

    /// <summary>
    /// Moves the asteroid downwards and horizontally.
    /// </summary>
    private void MoveAsteroid()
    {
        int starCount = PlayerCollusion.Instance.GetStarCollectCount();

        // Calculate speed based on the number of stars collected
        speed = 2.5f + (starCount / 5); // Increase speed for every 5 stars collected

        // Move the asteroid downwards at a constant speed with horizontal movement
        transform.Translate(new Vector3(horizontalMovementSpeed * Time.deltaTime, -speed * Time.deltaTime, 0));
    }

    /// <summary>
    /// Checks if the asteroid has moved out of the defined bounds and destroys it if so.
    /// </summary>
    private void DestroyOutOfBounds()
    {
        float xPos = 25f; // Right boundary
        float yPos = 20f; // Bottom boundary

        // Destroy the asteroid if it goes out of bounds
        if (IsOutOfBounds(xPos, yPos))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Determines if the asteroid is out of the defined boundaries.
    /// </summary>
    /// <param name="xPos">The horizontal boundary limit.</param>
    /// <param name="yPos">The vertical boundary limit.</param>
    /// <returns>True if the asteroid is out of bounds; otherwise, false.</returns>
    private bool IsOutOfBounds(float xPos, float yPos)
    {
        return transform.position.x > xPos || transform.position.x < -xPos ||
               transform.position.y < -yPos; // Check if the asteroid is out of horizontal or vertical bounds
    }
}