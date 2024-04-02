using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject player;
    public GameObject virtualCamera;

    public void Start()
    {
        player = GameObject.Find("Player");
        virtualCamera = GameObject.Find("Virtual Camera");
    }
}
