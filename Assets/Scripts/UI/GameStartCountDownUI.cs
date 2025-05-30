using TMPro;
using UnityEngine;

/// <summary>
/// Manages the countdown UI displayed at the start of the game.
/// </summary>
public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText; // Text element to display the countdown timer

    /// <summary>
    /// Subscribes to the game state changes and hides the countdown UI initially.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; // Subscribe to state changes
        Hide(); // Hide the countdown UI initially
    }

    /// <summary>
    /// Handles changes in the game state to show or hide the countdown UI.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStartActive()) // Check if countdown is active
        {
            Show(); // Show the countdown UI
        }
        else
        {
            Hide(); // Hide the countdown UI
        }
    }

    /// <summary>
    /// Updates the countdown timer displayed in the UI.
    /// </summary>
    private void Update()
    {
        if (GameManager.Instance.IsCountDownToStartActive()) // Check if countdown is active
        {
            float countDownTimer = GameManager.Instance.GetCountDownToStartTimer(); // Get the remaining countdown time
            countDownText.text = Mathf.CeilToInt(countDownTimer).ToString(); // Update the text to show the countdown time
            if (countDownTimer <= 0f) // Hide UI when countdown reaches zero
            {
                Hide();
            }
        }
    }

    /// <summary>
    /// Displays the countdown UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true); // Activate the UI GameObject
    }

    /// <summary>
    /// Hides the countdown UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }
}