using System;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public static MissileController Instance { get; private set; }

    public event EventHandler OnMissleHit;

    [SerializeField] private float laserSpeed = 100f;

    private void Awake()
    {
        Instance = this; 
    }


    private void Update()
    {
        LaserForwardMove();
        CheckBoundary();
    }


    private void LaserForwardMove()
    {
        transform.Translate(Vector2.up * laserSpeed * Time.deltaTime);
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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameManager.Instance.InstantiateParticle(GameManager.Instance.explosionParticle, this.transform);
            Destroy(collision.gameObject);
            Destroy(this.gameObject); 
            OnMissleHit?.Invoke(this, EventArgs.Empty); 
        }
    }
}