using TMPro;
using UnityEngine;

/// <summary>
/// Manages the display of the player's score in the game.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    // Singleton instance for easy access
    public static ScoreUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText; // Text element to display the score

    private int score = 0; // Variable to hold the current score

    /// <summary>
    /// Initializes the ScoreUI instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Sets the initial score display based on collected stars.
    /// </summary>
    private void Start()
    {
        scoreText.text = "Score: " + PlayerCollusion.Instance.GetStarCollectCount().ToString(); // Display initial score
    }

    /// <summary>
    /// Updates the score display each frame if the game is playing.
    /// </summary>
    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying()) // Check if the game is currently active
        {
            score = PlayerCollusion.Instance.GetStarCollectCount(); // Get the current score from PlayerCollusion
            scoreText.text = "Score: " + score.ToString(); // Update the score display
        }
    }
}