using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TMP_Text experienceText;


    [SerializeField] private TextMeshProUGUI scoreText;
    public GameObject pausePanel;

    private int currentScore;

    void Awake()
    {
        currentScore = 0;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }



    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.maxValue = max;
        energySlider.value = Mathf.RoundToInt(current);
        energyText.text = energySlider.value + "/" + energySlider.maxValue;
    }

    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = Mathf.RoundToInt(current);
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }

    public void UpdateExperienceSlider(float current, float max)
    {
        experienceSlider.maxValue = max;
        experienceSlider.value = Mathf.RoundToInt(current);
        experienceText.text = experienceSlider.value + "/" + experienceSlider.maxValue;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "" + currentScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}