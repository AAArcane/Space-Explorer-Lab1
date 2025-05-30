using System;
using UnityEngine;

/// <summary>
/// Manages the overall game state and flow, including starting, pausing, and ending the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static GameManager Instance { get; private set; }

    // Enum representing the various states of the game
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state; // Current state of the game

    // Timers for state transitions
    private float waitingToStartTimer = 1f; // Time before starting countdown
    private float countDownToStartTimer = 3f; // Countdown duration before the game starts

    // Events for state changes and pause actions
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private bool isGamePaused = false; // Tracks whether the game is paused

    [Header("Particle effects")]
    // Prefabs for various particle effects in the game
    public GameObject explosionParticle; // Explosion effect
    public GameObject muzzleFlash; // Muzzle flash effect for firing
    public GameObject projectileSharp; // Sharp projectile effect (currently unused)
    public GameObject playerDeath; // Effect for player death
    public GameObject CollectStar; // Effect for collecting a star

    /// <summary>
    /// Initializes the GameManager instance and sets the initial game state.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
        state = State.WaitingToStart; // Initialize state
    }

    /// <summary>
    /// Subscribes to the pause action event from GameInput.
    /// </summary>
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameManager_OnPauseAction; // Subscribe to pause events
    }

    /// <summary>
    /// Toggles the game's paused state when the pause action is triggered.
    /// </summary>
    private void GameManager_OnPauseAction(object sender, System.EventArgs e)
    {
        TogglePausedGame(); // Call method to toggle pause state
    }

    /// <summary>
    /// Updates the game state based on the current state and timers.
    /// </summary>
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime; // Count down to starting the game
                if (waitingToStartTimer <= 0f)
                {
                    state = State.CountDownToStart; // Transition to countdown state
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Notify subscribers
                }
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime; // Count down to game start
                if (countDownToStartTimer <= 0f)
                {
                    state = State.GamePlaying; // Transition to playing state
                    Time.timeScale = 1f; // Ensure game time is running
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Notify subscribers
                    SpawnManager.Instance.StartSpawning(); // Start spawning game entities
                }
                break;

            case State.GamePlaying:
                // Check if the player is alive; if not, transition to game over
                if (!PlayerCollusion.Instance.IsPlayerAlive())
                {
                    state = State.GameOver; // Transition to game over state
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Notify subscribers
                }
                break;

            case State.GameOver:
                // Handle game over state (currently no actions defined)
                break;
        }
    }

    /// <summary>
    /// Checks if the game is currently in the playing state.
    /// </summary>
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying; // Return true if the game is playing
    }

    /// <summary>
    /// Checks if the countdown to start is currently active.
    /// </summary>
    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart; // Return true if in countdown state
    }

    /// <summary>
    /// Checks if the game is over.
    /// </summary>
    public bool IsGameOver()
    {
        return state == State.GameOver; // Return true if the game is over
    }

    /// <summary>
    /// Gets the remaining time on the countdown to start.
    /// </summary>
    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer; // Return the current countdown timer
    }

    /// <summary>
    /// Toggles the game's paused state.
    /// </summary>
    public void TogglePausedGame()
    {
        isGamePaused = !isGamePaused; // Toggle the paused state
        if (isGamePaused)
        {
            Time.timeScale = 0f; // Stop the game time
            OnGamePaused?.Invoke(this, EventArgs.Empty); // Notify subscribers
        }
        else
        {
            Time.timeScale = 1f; // Resume the game time
            OnGameUnPaused?.Invoke(this, EventArgs.Empty); // Notify subscribers
        }
    }

    /// <summary>
    /// Instantiates a particle effect at the specified position.
    /// </summary>
    /// <param name="particlePrefab">The particle prefab to instantiate.</param>
    /// <param name="position">The position to instantiate the particle at.</param>
    public void InstantiateParticle(GameObject particlePrefab, Transform position)
    {
        if (particlePrefab != null && position != null)
        {
            GameObject particle = Instantiate(particlePrefab, position.position, Quaternion.identity);
            Destroy(particle, 2f); // Destroy the particle after 2 seconds
        }
    }
}