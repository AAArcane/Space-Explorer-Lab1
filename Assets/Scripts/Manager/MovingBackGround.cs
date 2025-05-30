using UnityEngine;

/// <summary>
/// Handles the movement and repeating effect of a scrolling background in the game.
/// </summary>
public class MovingBackGround : MonoBehaviour
{
    // Speed at which the background moves
    private float speed = 2f;

    // Starting position of the background
    private Vector3 startPos;

    // The height at which the background should repeat
    private float repeatWidth;

    /// <summary>
    /// Initializes the starting position and repeat width of the background.
    /// </summary>
    private void Start()
    {
        startPos = transform.position; // Store the initial position of the background
        repeatWidth = GetComponent<BoxCollider2D>().size.y / 2; // Calculate the repeat width based on the collider size
    }

    /// <summary>
    /// Updates the background movement each frame.
    /// </summary>
    private void Update()
    {
        MoveBackGround(); // Move the background downwards
        RepeatBackGround(); // Check if the background needs to reset its position
    }

    /// <summary>
    /// Moves the background downwards based on the defined speed.
    /// </summary>
    private void MoveBackGround()
    {
        // Move the background downwards using the speed and delta time
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    /// <summary>
    /// Resets the background position to create a repeating effect.
    /// </summary>
    private void RepeatBackGround()
    {
        // Check if the background has moved past the point where it needs to repeat
        if (transform.position.y < startPos.y - repeatWidth)
        {
            // Reset the position to its starting point
            transform.position = startPos;
        }
    }
}