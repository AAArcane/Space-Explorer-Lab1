using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle godModeToggle;

    // PlayerPrefs keys
    private const string SOUND_EFFECTS_KEY = "SoundEffectsVolume";
    private const string MUSIC_KEY = "MusicVolume";
    private const string GOD_MODE_KEY = "GodMode";
    private const float DEFAULT_VOLUME = 0.75f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize with saved values or defaults
        soundEffectSlider.value = PlayerPrefs.GetFloat(SOUND_EFFECTS_KEY, DEFAULT_VOLUME);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, DEFAULT_VOLUME);

        // Initialize God Mode toggle without applying to player
        godModeToggle.isOn = PlayerPrefs.GetInt(GOD_MODE_KEY, 0) == 1;

        // Set initial volumes
        AudioManager.Instance.SetSoundEffectsVolume(soundEffectSlider.value);
        AudioManager.Instance.SetMusicVolume(musicSlider.value);

        // Add listeners
        soundEffectSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        godModeToggle.onValueChanged.AddListener(SetGodMode);

        Hide();
    }

    private void SetSoundEffectsVolume(float volume)
    {
        AudioManager.Instance.SetSoundEffectsVolume(volume);
        PlayerPrefs.SetFloat(SOUND_EFFECTS_KEY, volume);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
        PlayerPrefs.Save();
    }

    private void SetGodMode(bool isGodMode)
    {
        PlayerPrefs.SetInt(GOD_MODE_KEY, isGodMode ? 1 : 0);
        PlayerPrefs.Save();

        // Only apply if player exists
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetGodMode(isGodMode);
        }
    }

    public void RefreshGodModeToggle()
    {
        godModeToggle.isOn = PlayerPrefs.GetInt(GOD_MODE_KEY, 0) == 1;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        // Refresh toggle state when showing options
        RefreshGodModeToggle();
    }
}