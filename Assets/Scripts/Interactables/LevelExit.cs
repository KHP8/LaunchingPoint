using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : Interactable
{
    public GameObject nextRoom;
    private RoomHandler roomHandler;

    void Start()
    {
        promptMessage = "Exit Level";
        roomHandler = nextRoom.GetComponent<RoomHandler>();
    }

    public override void Interact()
    {
        roomHandler.virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 0;
        roomHandler.player.transform.position = roomHandler.spawnPoint.transform.position;
        roomHandler.virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 2;
    }
}
