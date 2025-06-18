using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    private float waitingToStartTimer = 1f; 
    private float countDownToStartTimer = 3f;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private bool isGamePaused = false; 

    [Header("Particle effects")]
    // Prefabs for various particle effects in the game
    public GameObject explosionParticle; // Explosion effect
    public GameObject muzzleFlash; // Muzzle flash effect for firing
    public GameObject projectileSharp; // Sharp projectile effect (currently unused)
    public GameObject playerDeath; // Effect for player death
    public GameObject CollectStar; // Effect for collecting a star




    private void Awake()
    {
        Instance = this; 
        state = State.WaitingToStart; 
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameManager_OnPauseAction; // Subscribe to pause events
    }

    private void GameManager_OnPauseAction(object sender, System.EventArgs e)
    {
        TogglePausedGame(); // Call method to toggle pause state
    }

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
                    EnemyManager.Instance.StartSpawning();
                    SpawnManager.Instance.StartSpawning(); 

                }
                break;

            case State.GamePlaying:
                if (!PlayerCollusion.Instance.IsPlayerAlive())
                {
                    state = State.GameOver; // Transition to game over state
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Notify subscribers
                    
                }
                break;

            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying; // Return true if the game is playing
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart; // Return true if in countdown state
    }

    public bool IsGameOver()
    {
        return state == State.GameOver; // Return true if the game is over
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer; // Return the current countdown timer
    }

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

    public void InstantiateParticle(GameObject particlePrefab, Transform position)
    {
        if (particlePrefab != null && position != null)
        {
            GameObject particle = Instantiate(particlePrefab, position.position, Quaternion.identity);
            Destroy(particle, 2f); // Destroy the particle after 2 seconds
        }
    }
    public void OnMissileImpact(Vector2 position)
    {
        InstantiateParticle(explosionParticle, this.transform);
    }
}