using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles everything related to selecting abilities in the ability menu.
/// FilterButtons() takes preloaded names and images in each category of abilities and updates
/// the buttons that are in the grid. The buttons remain invisible until they are given a name / image.
/// OnAbilitySelect() places the name of the selected ability into PlayerPrefs with they key "*AbilityType*".
/// - Austin
/// </summary>

public class Hotbar : MonoBehaviour
{
    public GameObject hotbarButton;
    public GameObject[] abilityButtons = new GameObject[12];
    public GameObject contentBox;

    public GameObject[] hotbarButtons;

    private Dictionary<string, Sprite> abilityImageMap = new Dictionary<string, Sprite>();

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
        // Adding the ability images into a map with their names as the key.
        for (int i = 0; i < pAbilNames.Length; i++)
            abilityImageMap.Add(pAbilNames[i], pAbilImage[i]);
        for (int i = 0; i < sAbilNames.Length; i++)
            abilityImageMap.Add(sAbilNames[i], sAbilImage[i]);
        for (int i = 0; i < spAbilNames.Length; i++)
            abilityImageMap.Add(spAbilNames[i], spAbilImage[i]);
        for (int i = 0; i < UabilNames.Length; i++)
            abilityImageMap.Add(UabilNames[i], uAbilImage[i]);


        for (int i = 0; i < abilityButtons.Length; i++)
        {
            abilityButtons[i].SetActive(true);
            abilityButtons[i].GetComponent<Button>().enabled = false;
        }


        DefaultButtons();
    }

    public void OnAbilitySelect(GameObject abilityButton)
    {
        PlayerPrefs.SetString(abilityButton.GetComponent<AbilitySelectHandler>().abilityType, abilityButton.name);

        activeButton.GetComponent<Image>().sprite = abilityButton.GetComponent<Image>().sprite;
        activeButton = null;
        DefaultButtons();

        Debug.Log(abilityButton.GetComponent<AbilitySelectHandler>().abilityType + PlayerPrefs.GetString(abilityButton.GetComponent<AbilitySelectHandler>().abilityType));
    }

    private void DisableButtons()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
            abilityButtons[i].SetActive(false);
    }

    public void DefaultButtons()
    {
        contentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((defaultNames.Length / 4) * 250) + 300);

        if (PlayerPrefs.HasKey("Primary"))
            hotbarButtons[0].GetComponent<Image>().sprite = abilityImageMap[PlayerPrefs.GetString("Primary")];
        else
            hotbarButtons[0].GetComponent<Image>().sprite = null;
        if (PlayerPrefs.HasKey("Secondary"))
            hotbarButtons[1].GetComponent<Image>().sprite = abilityImageMap[PlayerPrefs.GetString("Secondary")];
        else
            hotbarButtons[1].GetComponent<Image>().sprite = null;

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

    private void FilterButtons(string[] abilities, Sprite[] images, GameObject[] descriptions, string type)
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
            abilityButtons[i].GetComponent<AbilitySelectHandler>().abilityType = type;
        }
    }

    // This is used by the MainMenuInputHelper file. Press ESC to deselect the active hotbar button and unequip current ability.
    public void DeselectButtons()
    {
        if (activeButton != null)
        {
            PlayerPrefs.DeleteKey(activeButton.name);
            activeButton = null;
            DefaultButtons();
        }
    }

    public void OnPrimaryButton(GameObject selectedButton)
    {
        DisableButtons();
        FilterButtons(pAbilNames, pAbilImage, primaryDesc, "Primary");
        activeButton = selectedButton;
    }

    public void OnSecondaryButton(GameObject selectedButton)
    {
        DisableButtons();
        FilterButtons(sAbilNames, sAbilImage, secondaryDesc, "Secondary");
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
