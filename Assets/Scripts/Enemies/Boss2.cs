using UnityEngine;

public class Boss2 : Enemy
{
    private Animator animator;
    private bool charging = true;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        EnterIdleState();
    }
    public override void Start()
    {
        base.Start();
        destroyEffectPool = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.hitArmor;
        destroySound = AudioManager.Instance.boom2;
    }

    public override void Update()
    {
        base.Update();
        float playerPosition = PlayerController.Instance.transform.position.x;

        if (transform.position.y > 4 || transform.position.y < -4)
        {
            speedY *= -1;
        }
        if (transform.position.x > 7.5)
        {
            EnterIdleState();
        }
        else if (transform.position.x < -8 || transform.position.x < playerPosition)
        {
            EnterChargeState();
        }
    }

    private void EnterIdleState()
    {
        if (charging)
        {
            speedX = 0.2f;
            speedY = Random.Range(-1.2f, 3f);
            charging = false;
            animator.SetBool("Charging", false);
        }
    }
    private void EnterChargeState()
    {
        if (!charging)
        {
            speedX = Random.Range(3.5f, 4f);
            speedY = 0f;
            charging = true;
            animator.SetBool("Charging", true);
        }
    }
}
