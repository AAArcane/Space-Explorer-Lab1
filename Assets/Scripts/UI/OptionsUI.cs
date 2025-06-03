using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;

    private void Awake()
    {
        soundEffectButton.onClick.AddListener(() =>
        {

        })
     ;
    }
}
