using System;
using UnityEngine;

/// <summary>
/// Manages audio playback for various game events such as missile firing and collisions.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static SoundManager Instance { get; private set; }


    private float volume = 1f; // Default volume level
    // Audio clips for different game events
    public AudioClip missileFiredSound;  // Sound played when a missile is fired
    [SerializeField] private AudioClip asteroidHitSound;    // Sound played when an asteroid is hit
    [SerializeField] private AudioClip playerDeathSound;     // Sound played when the player dies
    [SerializeField] private AudioClip collectStarSound;     // Sound played when a star is collected

    /// <summary>
    /// Initializes the SoundManager instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Subscribes to events for playing sounds based on game actions.
    /// </summary>
    private void Start()
    {
        PlayerAttack.Instance.OnMissileFired += PlayerAtttack_OnMissileFired; // Subscribe to missile fired event
        PlayerCollusion.Instance.OnPlayerDeath += PlayerCollusion_OnPlayerDeath; // Subscribe to player death event
        PlayerCollusion.Instance.OnStarCollected += PlayerCollusion_OnStarCollected; // Subscribe to star collected event
        MissileController.Instance.OnMissleHit += MissileController_OnMissileHit; // Subscribe to missile hit event
    }

    /// <summary>
    /// Plays the sound associated with a missile hitting an asteroid.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    public void MissileController_OnMissileHit(object sender, EventArgs e)
    {
        if (MissileController.Instance != null)
        {
            PlaySound(asteroidHitSound, MissileController.Instance.transform.position); // Play the asteroid hit sound
        }
    }

    /// <summary>
    /// Plays the sound associated with collecting a star.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void PlayerCollusion_OnStarCollected(object sender, EventArgs e)
    {
        PlaySound(collectStarSound, PlayerCollusion.Instance.transform.position); // Play the star collected sound
    }

    /// <summary>
    /// Plays the sound associated with the player’s death.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void PlayerCollusion_OnPlayerDeath(object sender, System.EventArgs e)
    {
        PlaySound(playerDeathSound, Camera.main.transform.position); // Play the player death sound
    }

    /// <summary>
    /// Plays the sound associated with firing a missile.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void PlayerAtttack_OnMissileFired(object sender, System.EventArgs e)
    {
        PlaySound(missileFiredSound, PlayerAttack.Instance.transform.position); // Play the missile fired sound
    }

    /// <summary>
    /// Plays a sound at a specified position.
    /// </summary>
    /// <param name="audioClip">The audio clip to play.</param>
    /// <param name="position">The position to play the sound at.</param>
    /// <param name="volume">The volume of the sound (default is 1).</param>
    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        // Ensure the final volume is not negative
        float finalVolume = Mathf.Max(0f, volumeMultiplier * volume);
        AudioSource.PlayClipAtPoint(audioClip, position, finalVolume); // Play the audio clip at the specified position
    }

    public void ChangeVolume()
    {
        volume += 1f;
        if (volume > 10f)
        {
            volume = 0f;
        }
    }

    public float GetVolume()
    {
        return volume; 
    }
}