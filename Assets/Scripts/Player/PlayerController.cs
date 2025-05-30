using UnityEngine;

/// <summary>
/// Controls the player's movement within the game.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves
    private float speed = 10f;

    /// <summary>
    /// Updates the player's position each frame based on input.
    /// </summary>
    private void Update()
    {
        PlayerMovement(); // Call method to handle player movement
    }

    /// <summary>
    /// Handles the player movement based on input from the GameInput class.
    /// </summary>
    private void PlayerMovement()
    {
        // Get the normalized movement vector from GameInput
        Vector2 movementInput = GameInput.Instance.GetMovementVectorNormalized();

        // Create a movement vector and apply speed
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime;
        transform.Translate(movement); // Move the player based on the calculated movement vector
    }
}