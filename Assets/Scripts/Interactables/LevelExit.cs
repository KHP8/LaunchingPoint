using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelExit : Interactable
{
    public GameObject nextRoom;
    private RoomHandler roomHandler;
    private WaitForSeconds cameraDelay = new WaitForSeconds(1f);
    public bool canExit = false;

    public GameObject winScreen;

    void Start()
    {
        if (nextRoom != null)
            roomHandler = nextRoom.GetComponent<RoomHandler>();
        else
        {
            roomHandler = null;
            winScreen = GameObject.Find("WinScreen");
        }

    }

    public override void Interact()
    {
        if (!canExit)
            return;

        if (roomHandler == null)
        {
            winScreen.transform.localScale = Vector3.one;
            gameObject.GetComponent<PauseMenu>().canPause = false;
            gameObject.GetComponent<PauseMenu>().isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        roomHandler.ActivateSpawners();
        roomHandler.virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 0;
        roomHandler.player.transform.position = roomHandler.spawnPoint.transform.position;
        roomHandler.player.transform.rotation = roomHandler.spawnPoint.transform.rotation;
        roomHandler.player.GetComponent<PlayerHealth>().RestoreHealth(100);
        StartCoroutine(teleportCamera());
    }


    public IEnumerator teleportCamera()
    {
        yield return cameraDelay;
        roomHandler.virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 2;
    }
}
