using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Hotbar : MonoBehaviour
{
    public GameObject hotbarButton;
    public GameObject[] abilityButtons = new GameObject[12];
    public GameObject contentBox;

    private GameObject activeButton;

    public string[] pAbilNames = new string[1];
    public string[] sAbilNames = new string[0];
    public string[] spAbilNames = new string[0];
    public string[] UabilNames = new string[0];

    public Sprite[] pAbilImage = new Sprite[1];
    public Sprite[] sAbilImage = new Sprite[0];
    public Sprite[] spAbilImage = new Sprite[0];
    public Sprite[] uAbilImage = new Sprite[0];

    public GameObject[] primaryDesc = new GameObject[1];
    public GameObject[] secondaryDesc = new GameObject[0];
    public GameObject[] specialDesc = new GameObject[0];
    public GameObject[] ultimateDesc = new GameObject[0];

    public string[] defaultNames = new string[12];
    public Sprite[] defaultImage = new Sprite[12];
    public GameObject[] defaultDesc = new GameObject[12];

    public void Awake()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            abilityButtons[i].SetActive(true);
            abilityButtons[i].GetComponent<Button>().enabled = false;
        }

        DefaultButtons();

        //pAbilNames[0] = "Fireball";
        //pAbilImage[0] = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;
    }

    private void DisableButtons()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
            abilityButtons[i].SetActive(false);
    }

    public void DefaultButtons()
    {
        contentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((defaultNames.Length / 4) * 250) + 300);

        for (int i = 0; i < defaultNames.Length; i++)
        {
            abilityButtons[i].SetActive(true);
            abilityButtons[i].GetComponent<Button>().enabled = false;
            abilityButtons[i].name = defaultNames[i];
            abilityButtons[i].GetComponent<Image>().sprite = defaultImage[i];
            abilityButtons[i].GetComponent<SettingsHoverInfo>().setting = abilityButtons[i];
            abilityButtons[i].GetComponent<SettingsHoverInfo>().description = defaultDesc[i];
        }
    }

    private void FilterButtons(string[] abilities, Sprite[] images, GameObject[] descriptions)
    {
        contentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((abilities.Length / 3) * 250 ) + 250);

        for (int i = 0; i < abilities.Length; i++)
        {
            abilityButtons[i].SetActive(true);
            abilityButtons[i].GetComponent<Button>().enabled = true;
            abilityButtons[i].name = abilities[i];
            abilityButtons[i].GetComponent<Image>().sprite = images[i];
            abilityButtons[i].GetComponent<SettingsHoverInfo>().setting = abilityButtons[i];
            abilityButtons[i].GetComponent<SettingsHoverInfo>().description = descriptions[i];
        }
    }

    public void DeselectButtons()
    {
        if (activeButton != null)
        {
            activeButton = null;
            DefaultButtons();
        }
    }

    public void OnPrimaryButton(GameObject selectedButton)
    {
        DisableButtons();
        FilterButtons(pAbilNames, pAbilImage, primaryDesc);
        activeButton = selectedButton;
    }

    public void OnSecondaryButton(GameObject selectedButton)
    {
        DisableButtons();
        FilterButtons(sAbilNames, sAbilImage, secondaryDesc);
        activeButton = selectedButton;
    }

    public void OnSpecialButton(GameObject selectedButton)
    {
        DisableButtons();
        activeButton = selectedButton;
    }

    public void OnUltimateButton(GameObject selectedButton)
    {
        DisableButtons();
        activeButton = selectedButton;
    }

    public void OnReadyButton(GameObject selectedButton)
    {

    }
}
