using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input actions and provides methods to access input bindings.
/// </summary>
public class GameInput : MonoBehaviour
{

    private const string PLAYER_PREFS_INPUT_BINDINGS = "InputBindings"; 
    public static GameInput Instance { get; private set; }

    // Enum for defining different input actions
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Space,
        Pause
    }

    // Events for input actions
    public event EventHandler OnPauseAction; // Triggered when the pause action is performed
    public event EventHandler OnSpaceAction; // Triggered when the space action is performed

    // Player input actions reference
    private PlayerInputActions playerInputActions;

    /// <summary>
    /// Initializes the GameInput instance and sets up input actions.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        Instance = this; 
        playerInputActions = new PlayerInputActions(); 

        if (PlayerPrefs.HasKey(PLAYER_PREFS_INPUT_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_INPUT_BINDINGS));
        }

        playerInputActions.Enable(); 
        playerInputActions.Player.Space.performed += Space_performed; // Subscribe to space action
        playerInputActions.Player.Paused.performed += Paused_performed; // Subscribe to pause action
    }

    /// <summary>
    /// Invoked when the space action is performed.
    /// </summary>
    /// <param name="obj">The input action callback context.</param>
    private void Space_performed(InputAction.CallbackContext obj)
    {
        OnSpaceAction?.Invoke(this, EventArgs.Empty); // Trigger space action event
    }

    /// <summary>
    /// Cleans up input action subscriptions and disposes of resources.
    /// </summary>
    private void OnDestroy()
    {
        playerInputActions.Player.Paused.performed -= Paused_performed; // Unsubscribe from pause action
        playerInputActions.Player.Space.performed -= Space_performed; // Unsubscribe from space action
        playerInputActions.Disable(); // Disable player input actions
        playerInputActions.Dispose(); // Dispose of player input actions
    }

    /// <summary>
    /// Gets the normalized movement vector based on player input.
    /// </summary>
    /// <returns>A normalized Vector2 representing the movement direction.</returns>
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Read movement input
        return inputVector.normalized; // Return the normalized vector
    }

    /// <summary>
    /// Invoked when the pause action is performed.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void Paused_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty); // Trigger pause action event
    }

    /// <summary>
    /// Gets the binding text for a specified input action.
    /// </summary>
    /// <param name="binding">The binding to retrieve text for.</param>
    /// <returns>A string representing the binding's current input.</returns>
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString(); // Up movement binding
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString(); // Down movement binding
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString(); // Left movement binding
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString(); // Right movement binding
            case Binding.Space:
                return playerInputActions.Player.Space.bindings[0].ToDisplayString(); // Space action binding
            case Binding.Pause:
                return playerInputActions.Player.Paused.bindings[0].ToDisplayString(); // Pause action binding
            default:
                throw new ArgumentOutOfRangeException(nameof(binding), binding, null); // Handle unexpected binding
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1; // Up movement binding index
                break;
                case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2; // Down movement binding index
                break;
                case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3; // Left movement binding index
                break;
                case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4; // Right movement binding index
                break;
                case Binding.Space:
                inputAction = playerInputActions.Player.Space;
                bindingIndex = 0; // Space action binding index
                break;
                case Binding.Pause:
                inputAction = playerInputActions.Player.Paused;
                bindingIndex = 0; // Pause action binding index
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound(); 
                PlayerPrefs.SetString(PLAYER_PREFS_INPUT_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();
    }
}