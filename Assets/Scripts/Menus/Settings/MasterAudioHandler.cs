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
        if (PlayerPrefs.HasKey("MasterAudio"))
            slider.value = PlayerPrefs.GetFloat("MasterAudio");
        else
            slider.value = slider.maxValue;
    }

    public void OnSliderValueChanged()
    {
        audioMixer.SetFloat("MasterVolume", slider.value);
        PlayerPrefs.SetFloat("MasterAudio", slider.value);
    }
}
