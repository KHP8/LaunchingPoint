using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicAudioHandler : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            slider.value = PlayerPrefs.GetFloat("MusicVolume");
        else
            slider.value = slider.maxValue;
    }

    public void OnSliderValueChanged()
    {
        if (slider.value == -51)
            audioMixer.SetFloat("Music", -80);
        else
            audioMixer.SetFloat("Music", slider.value);

        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}
