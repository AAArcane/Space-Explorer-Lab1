using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the main menu user interface, handling button actions for playing the game,
/// quitting the application, and showing instructions.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton; // Button to start the game
    [SerializeField] private Button quitButton; // Button to quit the application
    [SerializeField] private Button instructionButton; // Button to show instructions

    /// <summary>
    /// Initializes button listeners and sets the time scale to normal.
    /// </summary>
    private void Awake()
    {
        // Add listener to the play button to load the game scene
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });

        // Add listener to the quit button to exit the application
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // Ensure the game runs at normal speed
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Sets up additional listeners when the menu is started.
    /// </summary>
    private void Start()
    {
        // Add listener to the instruction button to show the tutorial UI
        instructionButton.onClick.AddListener(() =>
        {
            TutorialUI.Instance.Show();
        });
    }
}