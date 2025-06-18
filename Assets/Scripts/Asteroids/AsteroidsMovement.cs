using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the movement and behavior of asteroids in the game.
/// </summary>
public class AsteroidsMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;

    [SerializeField] private float horizontalMovementRange = 1.0f;

    private float horizontalMovementSpeed;

    private void Start()
    {
        horizontalMovementSpeed = Random.Range(-horizontalMovementRange, horizontalMovementRange);
    }

    private void Update()
    {
        MoveAsteroid(); 
        DestroyOutOfBounds(); 
    }

    private void MoveAsteroid()
    {
        int starCount = PlayerCollusion.Instance.GetStarCollectCount();

        speed = 2.5f + (starCount / 5); // Increase speed for every 5 stars collected

        transform.Translate(new Vector3(horizontalMovementSpeed * Time.deltaTime, -speed * Time.deltaTime, 0));
    }

    private void DestroyOutOfBounds()
    {
        float xPos = 25f; // Right boundary
        float yPos = 20f; // Bottom boundary

        if (IsOutOfBounds(xPos, yPos))
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfBounds(float xPos, float yPos)
    {
        return transform.position.x > xPos || transform.position.x < -xPos ||
               transform.position.y < -yPos; // Check if the asteroid is out of horizontal or vertical bounds
    }
}