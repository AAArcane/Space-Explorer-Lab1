using System;
using UnityEngine;

/// <summary>
/// Manages player missile attacks in the game.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    // Singleton instance for easy access
    public static PlayerAttack Instance { get; private set; }

    // Event that is triggered when a missile is fired
    public event EventHandler OnMissileFired;

    // Prefab of the missile to be instantiated
    [SerializeField] private GameObject missile;

    // Point from which the missile will be fired
    [SerializeField] private Transform missileFirePoint;

    // Time in seconds before the missile is destroyed
    [SerializeField] private int destroyDelay = 10;

    // Point where the muzzle flash particle effect will be instantiated
    [SerializeField] private Transform muzzleFirePoint;

    /// <summary>
    /// Initializes the PlayerAttack instance and subscribes to input events.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
        GameInput.Instance.OnSpaceAction += GameManager_OnSpaceAction; // Subscribe to space key action
    }

    /// <summary>
    /// Handles the space key action to fire a missile.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void GameManager_OnSpaceAction(object sender, System.EventArgs e)
    {
        // Check if the game is currently active
        if (!GameManager.Instance.IsGamePlaying()) return;

        // Instantiate muzzle flash particle effect
        GameManager.Instance.InstantiateParticle(GameManager.Instance.muzzleFlash, muzzleFirePoint);

        // Create a new missile instance at the fire point
        GameObject spawnMissile = Instantiate(missile, missileFirePoint);
        spawnMissile.transform.SetParent(null); // Detach from the fire point

        // Subscribe to the missile hit event
        MissileController missileController = spawnMissile.GetComponent<MissileController>();
        if (missileController != null)
        {
            missileController.OnMissleHit += SoundManager.Instance.MissileController_OnMissileHit; // Play sound on hit
        }

        // Trigger the missile fired event
        OnMissileFired?.Invoke(this, EventArgs.Empty);

        // Schedule the missile for destruction after a delay
        Destroy(spawnMissile, destroyDelay);
    }
}