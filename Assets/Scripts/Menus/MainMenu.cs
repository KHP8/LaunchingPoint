using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject newsMenu;
    private bool newsMenuOpen = false;
    public GameObject[] bannerButtons = new GameObject[7];

    public void Start()
    {
        newsMenu.SetActive(false);
    }

    public void OnNewsButton()
    {
        newsMenu.SetActive(true);
        newsMenuOpen = true;
        for (int i = 0; i < bannerButtons.Length; i++)
        {
            bannerButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    public void OnCloseNewsButton()
    {
        newsMenu.SetActive(false);
        newsMenuOpen = false;
        for (int i = 0; i < bannerButtons.Length; i++)
        {
            bannerButtons[i].GetComponent<Button>().enabled = true;
        }
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnLoadButton()
    {

    }

    public void OnMultiplayerButton()
    {

    }

    public void OnSettingsButton()
    {

    }

    public void OnCreditsButton()
    {
        
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
