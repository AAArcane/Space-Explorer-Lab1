using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    private float speed = 10f;
    private Vector2 movementInput; 


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentHealth = maxHealth;
        HealthBarUI.Instance.SetMaxHealth(maxHealth);

    }

    private void Update()
    {
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        movementInput = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    public bool IsMovingLeft()
    {
        return movementInput.x < 0;
    }

    public bool IsMovingRight()
    {
        return movementInput.x > 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBarUI.Instance.SetHealth(currentHealth);
    }


    public int GetMaxHealth() => maxHealth; 
    
    public int GetCurrentHealth() => currentHealth; 
}