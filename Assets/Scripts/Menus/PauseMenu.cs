using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;

    public GameObject settingsMenu;
    public bool settingsOpen = false;

    public void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void PauseManager()
    {
        if (isPaused)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        settingsOpen = false;
        settingsMenu.SetActive(false);
    }

    // Main Buttons
    // ------------
    public void OnResumeButton()
    {
        UnPause();
        Debug.Log("Unpaused");
    }

    public void OnSettingsButton()
    {
        settingsOpen = true;
        settingsMenu.SetActive(true);
    }

    public void OnSaveButton()
    {

    }

    public void OnLoadButton()
    {

    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnCloseSettingsButton()
    {
        settingsOpen = false;
        settingsMenu.SetActive(false);
    }
}
