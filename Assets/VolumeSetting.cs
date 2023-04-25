using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public static VolumeSetting Instance;
    public float VolumeMax = 20f;
    public float Volumemin = -20;
    public AudioMixer SoundSetting;
    public GameObject SettingGroup;
    public GameObject HintLabel;

    private bool IsShowPanel;

    public Slider BGMSlider;
    public Slider SoundSlider;

    private void Awake()
    {
        if (VolumeSetting.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void Start()
    {
        BGMVolChange();
        SVolChange();
        if (SettingGroup != null)
            SettingGroup.SetActive(false);
        if (HintLabel != null) HintLabel.SetActive(true);
    }
    public void BGMVolChange()
    {
        SoundSetting.SetFloat("BGM", ((VolumeMax - Volumemin) * BGMSlider.value) + Volumemin);
    }

    public void SVolChange()
    {
        SoundSetting.SetFloat("SE", ((VolumeMax - Volumemin) * SoundSlider.value) + Volumemin);
    }

    public void TogglePanel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsShowPanel = !IsShowPanel;
            if (HintLabel != null) HintLabel.SetActive(!IsShowPanel);
            if (SettingGroup != null)
                SettingGroup.SetActive(IsShowPanel);
        }
    }
}
