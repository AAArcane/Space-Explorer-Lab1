using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 2.5f; // Base speed of the enemy
    [SerializeField] private float horizontalMovementRange = 1.0f; // Range for horizontal movement

    private float horizontalMovementSpeed;

    private void Start()
    {
        horizontalMovementSpeed = Random.Range(-horizontalMovementRange, horizontalMovementRange);
    }

    private void Update()
    {
        MoveEnemy();
        DestroyOutOfBounds();
    }

    private void MoveEnemy()
    {
        // Get the number of stars collected by the player
        int scoreCount = ScoreUI.Instance.GetScore();

        float speed = baseSpeed + (scoreCount/ 500f); // Use 5f for float division

        // Move the enemy
        transform.Translate(new Vector3(horizontalMovementSpeed * Time.deltaTime, -speed * Time.deltaTime, 0));
    }

    private void DestroyOutOfBounds()
    {
        float xPos = 30f; // Right and left boundary

        // Check if the enemy is out of bounds
        if (IsOutOfBounds(xPos))
        {
            Destroy(gameObject); // Destroy the enemy if out of bounds
        }
    }

    private bool IsOutOfBounds(float xPos)
    {
        // Check if the enemy's x position is outside the boundaries
        return transform.position.x > xPos || transform.position.x < -xPos;
    }
}