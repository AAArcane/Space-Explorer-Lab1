using TMPro;
using UnityEngine;

/// <summary>
/// Manages the display of the player's score in the game.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText; // Text element to display the score

    private int score = 0; // Variable to hold the current score

    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    private void Start()
    {
        scoreText.text = "";
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying()) 
        {
            scoreText.text = "" + score.ToString();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd; 
        scoreText.text = "" + score.ToString(); 
    }

    public int GetScore()
    {
        return score;
    }
}