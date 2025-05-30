using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the tutorial user interface that displays control instructions to the player.
/// </summary>
public class TutorialUI : MonoBehaviour
{
    // Singleton instance for easy access
    public static TutorialUI Instance { get; private set; }

    [SerializeField] private Button closeButton; // Button to close the tutorial

    // Text elements for displaying key bindings
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keySpaceText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    /// <summary>
    /// Initializes the TutorialUI instance.
    /// </summary>
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    /// <summary>
    /// Sets up the tutorial UI and adds listeners to the close button.
    /// </summary>
    private void Start()
    {
        Invoke(nameof(UpdateVisual), 0.1f); // Update visual bindings after a short delay
        Hide(); // Hide the tutorial UI initially

        closeButton.onClick.AddListener(() =>
        {
            Hide(); // Hide the tutorial when the close button is clicked
        });
    }

    /// <summary>
    /// Updates the visual text elements with the current key bindings.
    /// </summary>
    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up); // Update move up binding
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down); // Update move down binding
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left); // Update move left binding
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right); // Update move right binding
        keySpaceText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Space); // Update space binding
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause); // Update pause binding
    }

    /// <summary>
    /// Displays the tutorial UI.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true); // Activate the UI GameObject
    }

    /// <summary>
    /// Hides the tutorial UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }
}