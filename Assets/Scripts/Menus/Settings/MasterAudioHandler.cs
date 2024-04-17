using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterAudioHandler : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
            slider.value = PlayerPrefs.GetFloat("MasterVolume");
        else
            slider.value = slider.maxValue;
    }

    public void OnSliderValueChanged()
    {
        audioMixer.SetFloat("Master", slider.value);
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
    }

    
}
