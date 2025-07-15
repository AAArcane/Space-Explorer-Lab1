// GodModeToggleHandler.cs
using UnityEngine;
using UnityEngine.UI;

public class GodModeToggleHandler : MonoBehaviour
{
    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        PlayerController.Instance.SetGodMode(isOn);
    }
}