using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject newsMenu;
    public GameObject settingsMenu;
    public GameObject abilityMenu;

    //private bool newsMenuOpen;
    public GameObject[] bannerButtons = new GameObject[7];

    public void Start()
    {
        newsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        abilityMenu.SetActive(false);
        //newsMenuOpen = false;
    }

    public void OnNewsButton()
    {
        newsMenu.SetActive(true);
        //newsMenuOpen = true;
        for (int i = 0; i < bannerButtons.Length; i++)
        {
            bannerButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    public void OnCloseNewsButton()
    {
        newsMenu.SetActive(false);
        //newsMenuOpen = false;
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
        abilityMenu.SetActive(true);
    }

    public void OnCloseAbilityButton()
    {
        abilityMenu.SetActive(false);
    }

    public void OnMultiplayerButton()
    {

    }

    public void OnSettingsButton()
    {
        settingsMenu.SetActive(true);
        for (int i = 0; i < bannerButtons.Length; i++)
        {
            bannerButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    public void OnCloseSettingsButton()
    {
        settingsMenu.SetActive(false);
        for (int i = 0; i < bannerButtons.Length; i++)
        {
            bannerButtons[i].GetComponent<Button>().enabled = true;
        }
    }

    public void OnCreditsButton()
    {
        
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
