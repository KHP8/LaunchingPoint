using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXAudioHandler : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
            slider.value = PlayerPrefs.GetFloat("SFXVolume");
        else
            slider.value = slider.maxValue;
    }

    public void OnSliderValueChanged()
    {
        if (slider.value == -51)
            audioMixer.SetFloat("SFX", -80);
        else
            audioMixer.SetFloat("SFX", slider.value);

        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }
}
