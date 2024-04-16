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

    void Start()
    {
        roomHandler = nextRoom.GetComponent<RoomHandler>();
    }

    public override void Interact()
    {
        if (!canExit)
            return;

        //roomHandler.SpawnEnemies();
        roomHandler.virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 0;
        roomHandler.player.transform.position = roomHandler.spawnPoint.transform.position;
        roomHandler.player.transform.rotation = roomHandler.spawnPoint.transform.rotation;
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
