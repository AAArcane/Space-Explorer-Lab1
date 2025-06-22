using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler destroyEffectPool;
    [SerializeField] private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private int damage;
    [SerializeField] private int experienceToGive;

    protected AudioSource hitSound;
    protected AudioSource destroySound;

    protected float speedX = 0f;
    protected float speedY = 0f;
    public virtual void OnEnable()
    {
        lives = maxLives;

    }
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }
    }

    public virtual void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlayModifiedSound(hitSound);
        lives -= damage;
       if (lives > 0)
        {
            flashWhite.Flash();
        } else
        {
            AudioManager.Instance.PlayModifiedSound(destroySound);
            flashWhite.Reset();
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffectPool.transform.position = transform.position;
            destroyEffectPool.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            PlayerController.Instance.GainExperience(experienceToGive);
            gameObject.SetActive(false);
        }
    }
}
