using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RoomHandler : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject player;
    public GameObject virtualCamera;
    public List<GameObject> enemies = new();
    public List<GameObject> spawners = new();
    public int numEnemies = 2;

    public void Start()
    {
        player = GameObject.Find("Player");
        virtualCamera = GameObject.Find("Virtual Camera");
    }

    public void SpawnEnemies()
    {
        List<GameObject> enemyTypes = Resources.LoadAll("Prefabs/Enemies").Cast<GameObject>().ToList();
        int[] dirtySpawners = new int[spawners.Count];
        int selectedSpawner;
        int selectedEnemy;

        for (int i = 0; i < numEnemies; i++)
        {
            selectedEnemy = Random.Range(0, enemyTypes.Count);
            selectedSpawner = Random.Range(0, spawners.Count);

            while (dirtySpawners[selectedSpawner] == 1)
                selectedSpawner = Random.Range(0, spawners.Count - 1);

            dirtySpawners[selectedSpawner] = 1;

            enemies.Add(Instantiate(enemyTypes[selectedEnemy], spawners[selectedSpawner].transform));
        }
    }
}
