using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    private Rigidbody2D rb;

    private int damage; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckBoundary();
    }
    public void Fire(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        rb.linearVelocity = direction * speed; // Set the velocity to shoot towards the player

        // Rotate projectile to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damage = 5;
            PlayerController.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }


    private void CheckBoundary()
    {
        float xPos = 31;
        float yPos = 22f;

        if (IsOutOfBounds(xPos, yPos))
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfBounds(float xPos, float yPos)
    {
        return transform.position.x > xPos || transform.position.x < -xPos ||
               transform.position.y > yPos || transform.position.y < -yPos;
    }
}