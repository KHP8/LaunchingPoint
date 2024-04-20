using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private List<GameObject> roomList;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject virtualCamera;

    // Start is called before the first frame update
    void Awake()
    {
        roomList = Resources.LoadAll("Prefabs/RoomsReal").Cast<GameObject>().ToList();

        GameObject[] rooms = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            GameObject room = roomList[i];
            Vector3 position = new Vector3(0, 0, 250 * (i + 1));
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            if (i > 1)
                rooms[i - 2].GetComponentInChildren<LevelExit>().nextRoom = rooms[i - 1];

            room.GetComponent<RoomHandler>().player = player;
            room.GetComponent<RoomHandler>().virtualCamera = virtualCamera;
            rooms[i] = Instantiate(room, position, rotation);
        }

        rooms[2].GetComponentInChildren<LevelExit>().nextRoom = rooms[3];
        rooms[3].GetComponentInChildren<LevelExit>().nextRoom = rooms[0];

        rooms[0].GetComponent<RoomHandler>().ActivateSpawners();

        virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 0;
        //Debug.Log(rooms[0].GetComponent<RoomHandler>().spawnPoint.transform.position);
        player.transform.position = rooms[0].GetComponent<RoomHandler>().spawnPoint.transform.position;
        player.transform.rotation = rooms[0].GetComponent<RoomHandler>().spawnPoint.transform.rotation;
        //Debug.Log("TEST");
        Debug.Log(player.transform.position);   
        virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
            .DampingFromCollision = 2;
    }
}
