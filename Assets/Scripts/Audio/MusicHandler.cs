using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public List<AudioClip> tracks = new();
    public AudioClip currentTrack;
    public int currentTrackIndex = 0;
    public AudioSource source;
    public MusicPlayerHandler musicPlayer;

    public void Start()
    {
        tracks = Resources.LoadAll<AudioClip>("Audio/Music").ToList();
        currentTrackIndex = 0;
        currentTrack = tracks[currentTrackIndex];
        source = GetComponentInParent<AudioSource>();
        musicPlayer.SetTitle(currentTrack.name);
        //musicPlayer = GameObject.Find("MusicControlButtons").GetComponent<MusicPlayerHandler>();
    }

    public void UpdateCurrentTrack()
    {
        source.clip = currentTrack;
        PlayTrack();
    }


    // For testing purposes. Do not use.
    public void UpdateCurrentTrack(AudioClip track)
    {

    }

    private void UpdateMusicPlayer()
    {
        musicPlayer.ChangePlayPauseSprite();
        musicPlayer.UpdateMusicProgress();
        musicPlayer.SetTitle(currentTrack.name);
    }

    public void AutoNextTrack()
    {
        if (!source.isPlaying)
            NextTrack();
    }

    public void NextTrack()
    {
        if ((currentTrackIndex + 1) >= tracks.Count)
            currentTrackIndex = 0;
        else
            currentTrackIndex++;

        currentTrack = tracks[currentTrackIndex];
        UpdateCurrentTrack();
        UpdateMusicPlayer();
    }

    public void PreviousTrack()
    {
        if ((currentTrackIndex - 1) < 0)
            currentTrackIndex = tracks.Count - 1;
        else
            currentTrackIndex--;

        currentTrack = tracks[currentTrackIndex];
        UpdateCurrentTrack();
        UpdateMusicPlayer();
    }

    // For buttons
    public void PlayPauseToggle()
    {
        if (source.isPlaying)
            PauseTrack();
        else
            PlayTrack();

        UpdateMusicPlayer();
    }

    private void PlayTrack()
    {
        source.Play();
    }

    private void PauseTrack()
    {
        source.Pause();
    }
}
