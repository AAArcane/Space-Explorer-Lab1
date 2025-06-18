using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public static HealthBarUI Instance { get; private set; }

    [SerializeField] private Slider slider;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Image fillImage;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fillImage.color = gradient.Evaluate(1f); 
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fillImage.color = gradient.Evaluate(slider.normalizedValue); 
    }
}
