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
            unPause();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void unPause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMenuButton()
    {
        if (isPaused)
            SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitButton()
    {   if (isPaused)
            Application.Quit();
    }
}
