using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the user interface displayed when the game is paused,
/// allowing the player to resume the game or return to the main menu.
/// </summary>
public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton; // Button to resume the game
    [SerializeField] private Button mainMenuButton; // Button to return to the main menu

    /// <summary>
    /// Initializes button click listeners.
    /// </summary>
    private void Awake()
    {
        // Add listener to resume the game when the resume button is clicked
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePausedGame(); // Toggle the paused state
        });

        // Add listener to load the main menu when the main menu button is clicked
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene); // Load the main menu scene
        });
    }

    /// <summary>
    /// Subscribes to game pause and unpause events and hides the UI initially.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused; // Subscribe to pause event
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused; // Subscribe to unpause event
        Hide(); // Hide the paused UI initially
    }

    /// <summary>
    /// Handles the game unpause event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide(); // Hide the paused UI when the game resumes
    }

    /// <summary>
    /// Handles the game pause event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show(); // Show the paused UI when the game is paused
    }

    private void Show()
    {
        gameObject.SetActive(true); // Activate the UI GameObject
    }

   
    private void Hide()
    {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }
}