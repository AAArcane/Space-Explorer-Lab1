using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; } 

    private AudioSource audioSource;
    private float volume = 3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeVolume()
    {
        volume += 1f; // Increase volume by 0.1
        if (volume > 10f) // Ensure volume does not exceed 1
        {
            volume = 0f; // Reset volume to 0 if it exceeds 1
        }
        audioSource.volume = volume; 
    }

    public float GetVolume()
    {
        return volume; // Return the current volume level
    }
}
