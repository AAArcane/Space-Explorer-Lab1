using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the game over user interface, displaying collected stars and providing options to restart or go to the main menu.
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starCollectedText; // Text element to display the number of stars collected
    [SerializeField] private Button restartButton; // Button to restart the game
    [SerializeField] private Button mainMenuButton; // Button to return to the main menu

    /// <summary>
    /// Initializes the buttons' click listeners.
    /// </summary>
    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
        mainMenuButton.onClick.AddListener(() =>
        {
           SceneManager.LoadScene("MainMenuScene"); // Load the main menu scene when the button is clicked
        });
    }

    /// <summary>
    /// Subscribes to the game manager's state change event and hides the UI initially.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; // Subscribe to state changes
        Hide(); // Hide the game over UI initially
    }

    /// <summary>
    /// Handles changes in the game state.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver()) // Check if the game is over
        {
            Show(); // Show the game over UI
            starCollectedText.text = PlayerCollusion.Instance.GetStarCollectCount().ToString(); // Display the number of collected stars
        }
        else
        {
            Hide(); // Hide the game over UI if the game is not over
        }
    }

    /// <summary>
    /// Displays the game over UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true); // Activate the UI GameObject
    }

    /// <summary>
    /// Hides the game over UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }
}