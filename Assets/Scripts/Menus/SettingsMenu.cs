using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject displayButton;
    public GameObject graphicsButton;
    public GameObject audioButton; 
    public GameObject gameplayButton;

    public GameObject displayTab;
    public GameObject graphicsTab;
    public GameObject audioTab;
    public GameObject gameplayTab;

    public void Start()
    {
        OnDisplayButton();
    }

    private void ButtonAlphaChange(float displayA, float graphicsA, float audioA, float gameplayA)
    {
        Color tempColor;
        
        tempColor = displayButton.GetComponent<Image>().color;
        tempColor.a = displayA;
        displayButton.GetComponent<Image>().color = tempColor;

        tempColor = graphicsButton.GetComponent<Image>().color;
        tempColor.a = graphicsA;
        graphicsButton.GetComponent<Image>().color = tempColor;

        tempColor = audioButton.GetComponent<Image>().color;
        tempColor.a = audioA;
        audioButton.GetComponent<Image>().color = tempColor;

        tempColor = gameplayButton.GetComponent<Image>().color;
        tempColor.a = gameplayA;
        gameplayButton.GetComponent<Image>().color = tempColor;
    }

    private void ActiveTab(bool display, bool graphics, bool audio, bool gameplay)
    {
        displayTab.SetActive(display);
        graphicsTab.SetActive(graphics);
        audioTab.SetActive(audio);
        gameplayTab.SetActive(gameplay);
    }

    public void OnDisplayButton()
    {
        ButtonAlphaChange(1, .4377f, .4377f, .4377f);
        ActiveTab(true, false, false, false);  
    }

    public void OnGraphicsButton()
    {
        ButtonAlphaChange(.4377f, 1, .4377f, .4377f);
        ActiveTab(false, true, false, false);
    }

    public void OnAudioButton()
    {
        ButtonAlphaChange(.4377f, .4377f, 1, .4377f);
        ActiveTab(false, false, true, false);
    }

    public void OnGameplayButton()
    {
        ButtonAlphaChange(.4377f, .4377f, .4377f, 1);
        ActiveTab(false, false, false, true);
    }
}
