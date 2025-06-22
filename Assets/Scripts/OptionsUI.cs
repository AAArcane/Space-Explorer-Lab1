using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;

    // PlayerPrefs keys
    private const string SOUND_EFFECTS_KEY = "SoundEffectsVolume";
    private const string MUSIC_KEY = "MusicVolume";
    private const float DEFAULT_VOLUME = 0.75f; // Default volume if no PlayerPrefs exist

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize with saved values or defaults
        soundEffectSlider.value = PlayerPrefs.GetFloat(SOUND_EFFECTS_KEY, DEFAULT_VOLUME);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, DEFAULT_VOLUME);

        // Set initial volumes in AudioManager
        AudioManager.Instance.SetSoundEffectsVolume(soundEffectSlider.value);
        AudioManager.Instance.SetMusicVolume(musicSlider.value);

        // Add listeners
        soundEffectSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        Hide();
    }

    private void SetSoundEffectsVolume(float volume)
    {
        AudioManager.Instance.SetSoundEffectsVolume(volume);
        PlayerPrefs.SetFloat(SOUND_EFFECTS_KEY, volume);
        PlayerPrefs.Save();  // Explicitly save to disk
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
        PlayerPrefs.Save();  // Explicitly save to disk
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}