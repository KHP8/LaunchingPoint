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
    public int numEnemies;
    public int maxEnemies;

    public void Start()
    {
        player = GameObject.Find("Player");
        virtualCamera = GameObject.Find("Virtual Camera");
    }

    public void ActivateSpawners()
    {
        List<GameObject> enemyTypes = Resources.LoadAll("Prefabs/Enemies").Cast<GameObject>().ToList();
        int[] dirtySpawners = new int[spawners.Count];
        int selectedSpawner;
        int selectedEnemy;

        for (int i = 0; i < maxEnemies; i++)
        {
            selectedEnemy = Random.Range(0, enemyTypes.Count);
            selectedSpawner = Random.Range(0, spawners.Count);

            if (spawners[selectedSpawner].GetComponent<EnemySpawner>().currentEnemyCount >= spawners[selectedSpawner].GetComponent<EnemySpawner>().maxEnemyCount)
                dirtySpawners[selectedSpawner] = 1;

            while (dirtySpawners[selectedSpawner] == 1)
                selectedSpawner = Random.Range(0, spawners.Count - 1);

            Debug.Log("enemyTypes: " + selectedEnemy);
            Debug.Log("selectedSpawner: " + selectedSpawner);
            spawners[selectedSpawner].GetComponent<EnemySpawner>()
                .SpawnEnemy(enemyTypes[selectedEnemy], enemies, this.gameObject);
            numEnemies++;
        }

        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().LoadStyles();
        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveIncomplete();
        UpdateObjective();
    }

    public void UpdateObjective()
    {
        string objectiveText = "Defeat Enemies\n" + (maxEnemies - enemies.Count) + " / " + maxEnemies;
        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveText(objectiveText);
        if (numEnemies == 0)
        {
            GetComponentInChildren<LevelExit>().canExit = true;
            GetComponentInChildren<LevelExit>().promptMessage = "Exit Level";
            GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveComplete();
        }
    }
}
