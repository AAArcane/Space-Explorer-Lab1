using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource; 
    private float _soundEffectsVolume = 1f;
    private float _musicVolume = 1f;

    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource squished;
    public AudioSource burn;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource bossSpawn;
    public AudioSource beetleDestroy;
    public AudioSource beetleHit;
    public AudioSource locusCharge;
    public AudioSource locusHit;
    public AudioSource locusDestroy;
    public AudioSource squidDestroy;
    public AudioSource squidDestroy2;
    public AudioSource squidHit;
    public AudioSource squidHit2;
    public AudioSource squidShoot;
    public AudioSource squidShoot2;
    public AudioSource squidShoot3;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }

        // Initialize music volume from source
        if (musicSource != null)
        {
            _musicVolume = musicSource.volume;
        }
    }

    public void PlaySound(AudioSource sound){
        sound.volume = _soundEffectsVolume;
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound){
        sound.volume = _soundEffectsVolume;
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }

    public void SetSoundEffectsVolume(float volume)
    {
        _soundEffectsVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        if (musicSource != null)
        {
            musicSource.volume = _musicVolume;
        }
    }
    public float GetSoundEffectsVolume() => _soundEffectsVolume;
    public float GetMusicVolume() => _musicVolume;
}
