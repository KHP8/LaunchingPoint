using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityCastSFX : MonoBehaviour
{
    public List<AudioSource> sources = new();

    public void PlayAbilityAudio(AudioClip sfx)
    {
        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i] == null || sources[i].isPlaying == false)
            {
                Debug.Log(sfx);
                sources[i].clip = sfx;
                sources[i].Play();
                return;
            }
        }
    }
}
