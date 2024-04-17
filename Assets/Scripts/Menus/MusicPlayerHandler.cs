using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerHandler : MonoBehaviour
{
    public GameObject playPauseButton;
    public GameObject title;
    public GameObject artist;

    public Sprite pauseSprite;
    public Sprite resumeSprite;
    public GameObject trackSlider;
    public Coroutine coro;

    public void ChangePlayPauseSprite()
    {
        if (GameObject.Find("PlayerMusic").GetComponent<AudioSource>().isPlaying)
            playPauseButton.GetComponent<Image>().sprite = pauseSprite;
        else
            playPauseButton.GetComponent<Image>().sprite = resumeSprite;
    }

    public void SetTitle(string trackTitle)
    {
        title.GetComponent<TextMeshProUGUI>().text = trackTitle;
    }

    public void UpdateMusicProgress()
    {
        if (coro != null)
            StopCoroutine(coro);

        coro = StartCoroutine(UpdateMusicProgressHelper());
    }

    private IEnumerator UpdateMusicProgressHelper()
    {
        while (GameObject.Find("PlayerMusic").GetComponent<AudioSource>().isPlaying)
        {
            trackSlider.GetComponent<Slider>().value =
                GameObject.Find("PlayerMusic").GetComponent<AudioSource>().time /
                GameObject.Find("PlayerMusic").GetComponent<MusicHandler>().currentTrack.length;
            yield return new WaitForSeconds(1);
        }
    }
}
