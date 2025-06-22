using System.Collections.Generic;
using UnityEngine;

public class LocusMorph : Enemy
{
    [SerializeField] private List<Frames> frames;
    private int enemyVariant;
    private bool isCharging;
    public override void OnEnable()
    {
        base.OnEnable();
        enemyVariant = Random.Range(0, frames.Count);
        EnterIdle();
    }
    public override void Start()
    {
        base.Start();
        destroyEffectPool = GameObject.Find("LocustPopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.locusHit;
        destroySound = AudioManager.Instance.locusDestroy;

        speedX = Random.Range(0.1f, 0.6f);
        speedY = Random.Range(-0.9f, 0.9f);
    }

    public override void Update()
    {
        base.Update();
        if (transform.position.y > 5 || transform.position.y < -5f)
        {
            speedY *= -1; // Reverse direction if out of bounds
        }
    }

    private void EnterIdle()
    {
        isCharging = false;
        spriteRenderer.sprite = frames[enemyVariant].sprite[0];
        speedX = Random.Range(0.1f, 0.6f);
        speedY = Random.Range(-0.9f, 0.9f);
    }

    private void EnterCharge()
    {
        if (!isCharging){
            isCharging = true;
            spriteRenderer.sprite = frames[enemyVariant].sprite[1];
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.locusCharge);
        speedX = Random.Range(-4f, -6f);
            speedY = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if ( lives <= maxLives * 0.5f)
        {
            EnterCharge();
        }
    }

    [System.Serializable]   
    private class Frames
    {
        public Sprite[] sprite;
    }
}
