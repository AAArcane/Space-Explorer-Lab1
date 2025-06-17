using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_ATTACK = "IsAttack";
    private const string IS_MOVINGLEFT = "IsMovingLeft";
    private const string IS_MovingRight = "IsMovingRight";

    private Animator animator;
    [SerializeField] private PlayerAttack PlayerAttack;
    [SerializeField] private PlayerController playerController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool(IS_ATTACK, false);

        animator.SetBool(IS_MOVINGLEFT, false);
        animator.SetBool(IS_MovingRight, false);
    }
    private void Update()
    {
        animator.SetBool(IS_ATTACK, PlayerAttack.IsAttack());
        animator.SetBool(IS_MOVINGLEFT, playerController.IsMovingLeft());
        animator.SetBool(IS_MovingRight, playerController.IsMovingRight());
    }
}