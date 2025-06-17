using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private float speed = 10f;
    private Vector2 movementInput; // Stores the current movement input

    private void Awake()
    {
        Instance = this;
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
}