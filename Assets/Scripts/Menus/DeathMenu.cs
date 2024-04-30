using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

}
