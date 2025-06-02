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
    [SerializeField] private int maxMissiles = 3;
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

        // Get the number of stars collected
        int starCount = PlayerCollusion.Instance.GetStarCollectCount();


        // Determine the number of missiles to fire based on stars collected
        int missilesToFire = 1; // Default to 1 missile

        if (starCount >= 10) missilesToFire = 2; // Fire 2 missiles at 10 stars
        if (starCount >= 20) missilesToFire = 3; // Fire 3 missiles at 20 stars

        // Fire the missiles
        for (int i = 0; i < missilesToFire; i++)
        {
            // Determine the rotation and position based on the missile index
            Quaternion rotation;
            Vector3 spawnPosition = missileFirePoint.position; // Base position

            if (missilesToFire == 2)
            {
                // Fire two missiles at 30 and -30 degrees
                rotation = Quaternion.Euler(0, 0, i == 0 ? 20 : -20);
            }
            else if (missilesToFire == 3)
            {
                // Fire three missiles: -30, 0, and 30 degrees
                rotation = Quaternion.Euler(0, 0, i == 0 ? -20 : (i == 1 ? 0 : 20));
            }
            else
            {
                // Default case: fire 1 missile straight
                rotation = Quaternion.identity; // No rotation
            }

            // Create a new missile instance at the fire point with the calculated rotation
            GameObject spawnMissile = Instantiate(missile, spawnPosition, rotation);
            spawnMissile.transform.SetParent(null); // Detach from the fire point

            // Subscribe to the missile hit event
            MissileController missileController = spawnMissile.GetComponent<MissileController>();
            if (missileController != null)
            {
                missileController.OnMissleHit += SoundManager.Instance.MissileController_OnMissileHit; // Play sound on hit
            }

            // Trigger the missile fired event only for the first missile
            if (i == 0)
            {
                OnMissileFired?.Invoke(this, EventArgs.Empty);
            }

            // Schedule the missile for destruction after a delay
            Destroy(spawnMissile, destroyDelay);
        }
    }
}