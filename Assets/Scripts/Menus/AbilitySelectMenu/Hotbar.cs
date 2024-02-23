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

    public void Awake()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
            abilityButtons[i].SetActive(true);

        //pAbilNames[0] = "Fireball";
        //pAbilImage[0] = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;
    }

    private void DisableButtons()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
            abilityButtons[i].SetActive(false);
    }

    private void FilterButtons(string[] abilities, Sprite[] images, GameObject[] descriptions)
    {
        contentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((abilities.Length / 3) * 250 ) + 250);

        int activeButton = 0;
        for (int i = 0; i < abilities.Length; i++)
        {
            abilityButtons[activeButton].SetActive(true);
            abilityButtons[activeButton].name = abilities[i];
            abilityButtons[activeButton].GetComponent<Image>().sprite = images[i];
            abilityButtons[activeButton].GetComponent<SettingsHoverInfo>().setting = abilityButtons[activeButton];
            abilityButtons[activeButton].GetComponent<SettingsHoverInfo>().description = descriptions[i];
        }
    }

    public void OnPrimaryButton()
    {
        DisableButtons();
        FilterButtons(pAbilNames, pAbilImage, primaryDesc);
    }

    public void OnSecondaryButton()
    {
        DisableButtons();
    }

    public void OnSpecialButton()
    {
        DisableButtons();
    }

    public void OnUltimateButton()
    {
        DisableButtons();
    }

    public void OnReadyButton()
    {

    }
}
