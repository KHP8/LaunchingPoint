using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;

    public void Start()
    {
        pauseMenu.SetActive(false);
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
    }

    // Buttons
    // -------
    public void OnResumeButton()
    {
        UnPause();
    }

    public void OnSettingsButton()
    {

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
}
