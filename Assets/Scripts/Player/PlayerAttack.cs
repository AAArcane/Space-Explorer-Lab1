using System;
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance { get; private set; }

    public event EventHandler OnMissileFired;

    [Header("Attack Settings")]
    [SerializeField] private bool isAttack;
    [SerializeField] private float attackStateDuration = 0.5f;
    private Coroutine resetAttackCoroutine;

    [Header("Missile Settings")]
    [SerializeField] private GameObject missile;
    [SerializeField] private int maxMissiles = 3;
    [SerializeField] private Transform missileFirePoint;
    [SerializeField] private int destroyDelay = 10;

    [Header("Effects")]
    [SerializeField] private Transform muzzleFirePoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameInput.Instance.OnSpaceAction += GameManager_OnSpaceAction;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnSpaceAction -= GameManager_OnSpaceAction;
    }

    private void GameManager_OnSpaceAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (!isAttack)
        {
            GameManager.Instance.InstantiateParticle(GameManager.Instance.muzzleFlash, muzzleFirePoint);
            int starCount = PlayerCollusion.Instance.GetStarCollectCount();
            int missilesToFire = Mathf.Min(1 + starCount / 10, maxMissiles);

            for (int i = 0; i < missilesToFire; i++)
            {
                Quaternion rotation = GetMissileRotation(i, missilesToFire);
                Vector3 spawnPosition = missileFirePoint.position;

                GameObject spawnMissile = Instantiate(missile, spawnPosition, rotation);
                spawnMissile.transform.SetParent(null);

                // Setup missile controller
                if (spawnMissile.TryGetComponent(out MissileController missileController))
                {
                    missileController.OnMissleHit += SoundManager.Instance.MissileController_OnMissileHit;
                }

                // Only trigger events for the first missile
                if (i == 0)
                {
                    OnMissileFired?.Invoke(this, EventArgs.Empty);
                    SetAttackState();
                }

                Destroy(spawnMissile, destroyDelay);
            }
        }
    }

    private Quaternion GetMissileRotation(int index, int totalMissiles)
    {
        return totalMissiles switch
        {
            2 => Quaternion.Euler(0, 0, index == 0 ? 20 : -20),
            3 => Quaternion.Euler(0, 0, index == 0 ? -20 : (index == 1 ? 0 : 20)),
            _ => Quaternion.identity
        };
    }

    private void SetAttackState()
    {
        isAttack = true;
        
        if (resetAttackCoroutine != null)
        {
            StopCoroutine(resetAttackCoroutine);
        }
        
        resetAttackCoroutine = StartCoroutine(ResetAttackAfterDelay());
    }

    private IEnumerator ResetAttackAfterDelay()
    {
        yield return new WaitForSeconds(attackStateDuration);
        isAttack = false;
    }

    public bool IsAttack()
    {
        return isAttack;
    }
}