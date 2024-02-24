using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInputHelper : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.UIActions ui;

    public Hotbar hotbar;

    void Awake()
    {
        playerInput = new PlayerInput();
        ui = playerInput.UI;

        //hotbar = GetComponent<Hotbar>();

        ui.Cancel.performed
            += ctx => hotbar.DeselectButtons();

        UIEnable();
    }

    private void UIEnable()
    {
        ui.Enable();
    }

    private void UIDisable()
    {
        ui.Disable();
    }
}
