using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int roomsLeft; 

    private List<GameObject> roomList;
    private List<GameObject> doorList;
    private GameObject endWall;

    [HideInInspector]
    public Node previous;

    public int GetRoomsLeft()
    {
        if (previous)
            return previous.GetRoomsLeft();
        return roomsLeft;
    }

    public void DecrementRooms()
    {
        if (previous)
            previous.DecrementRooms();
        else
            --roomsLeft;
    }
    
    public void Start()
    {
        endWall = Resources.Load("Prefabs/EndWall") as GameObject;
        roomList = Resources.LoadAll("Prefabs/Rooms").Cast<GameObject>().ToList();
        doorList = Resources.LoadAll("Prefabs/Doors").Cast<GameObject>().ToList();

        if ( GetRoomsLeft() == 0 || SpaceFull()) // If no rooms left or 
        {
            Vector3 heightAdd = Vector3.up * endWall.transform.GetChild(0).transform.localScale.y / 2; //new Vector3(0, door.transform.GetChild(0).transform.localScale.y / 2, 0);
            Instantiate(endWall, gameObject.transform.position + heightAdd, gameObject.transform.rotation);
            return;  
        }

        BuildDoor();
        BuildRoom();
        DecrementRooms();

        //Transform nextNode = Room.transform.Find("Node");
        //nextNode.GetComponent<Node>().roomsLeft = --roomsLeft;

    }

    bool SpaceFull() {
        RaycastHit hitInfo;
        return Physics.SphereCast(gameObject.transform.position, 11, gameObject.transform.forward, out hitInfo, 12);
    }

    void BuildDoor()
    {
        // Build this door
        GameObject door = doorList[Random.Range(0, doorList.Count)];
        Vector3 heightAdd = Vector3.up * door.transform.GetChild(0).transform.localScale.y / 2; //new Vector3(0, door.transform.GetChild(0).transform.localScale.y / 2, 0);
        Instantiate(door, gameObject.transform.position + heightAdd, gameObject.transform.rotation);
    }

    void BuildRoom()
    {
        // Build next room
        GameObject room = roomList[Random.Range(0, roomList.Count)];
        Vector3 distAdd = gameObject.transform.forward * room.transform.Find("Floor").transform.localScale.z / 2; //new Vector3(0, 0, room.transform.GetChild(0).transform.localScale.z / 2);
        GameObject Room = Instantiate(room, gameObject.transform.position + distAdd, gameObject.transform.rotation);
        PrepNodes(Room);
    }

    void PrepNodes(GameObject room)
    {
        // Prep next nodes
        Transform [] activeTransforms = room.GetComponentsInChildren<Transform>();
        foreach(Transform x in activeTransforms) 
        {
            if (x.transform.CompareTag("Node"))
            {
                x.GetComponent<Node>().previous = this;
            }
        }

    }

}
