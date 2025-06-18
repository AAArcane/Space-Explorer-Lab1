using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[0];
    public float animationTime = 1f;
    public int score = 10;

    private int health = 3; 
    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationSprites[0];
    }

    private void Start()
    {
        health = 3; 
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

   private void Update()
    {
        OnEnemyHealthReachZero();
    }

    private void AnimateSprite()
    {
        animationFrame++;

        // Loop back to the start if the animation frame exceeds the length
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            health--;

        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
        }
    }

    private void OnEnemyHealthReachZero()
    { if (health <= 0)
        {
            ScoreUI.Instance.AddScore(score); 
            gameObject.SetActive(false); 
        }
    }
}
