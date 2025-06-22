using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;

    void Awake()
    {
            Instance = this;
        
    }

    void Start()
    {
        Hide(); 
        soundEffectSlider.value = AudioManager.Instance.GetSoundEffectsVolume();
        musicSlider.value = AudioManager.Instance.GetMusicVolume();

        soundEffectSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetSoundEffectsVolume(float volume)
    {
        AudioManager.Instance.SetSoundEffectsVolume(volume);
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void Hide()
    {
        // Hide the options UI
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // Show the options UI
        gameObject.SetActive(true);
    }
}