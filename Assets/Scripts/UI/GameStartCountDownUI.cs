using TMPro;
using UnityEngine;

/// <summary>
/// Manages the countdown UI displayed at the start of the game.
/// </summary>
public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUp";


    [SerializeField] private TextMeshProUGUI countDownText; // Text element to display the countdown timer

    private int previousCountDownNumber;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; // Subscribe to state changes
        Hide(); // Hide the countdown UI initially
    }

    
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
            int countDownTimer = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
            countDownText.text = countDownTimer.ToString();

            if (previousCountDownNumber != countDownTimer)
            {
                previousCountDownNumber = countDownTimer;
                animator.SetTrigger(NUMBER_POPUP);
            }
          
        }
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