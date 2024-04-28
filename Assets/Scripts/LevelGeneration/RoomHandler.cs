using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RoomHandler : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject player;
    public GameObject virtualCamera;
    public List<GameObject> enemies = new();
    public List<GameObject> spawners = new();
    public GameObject bossSpawner;

    public int numEnemies;
    public int maxEnemies;

    public bool isBossRoom;

    public void Start()
    {
        player = GameObject.Find("Player");
        virtualCamera = GameObject.Find("Virtual Camera");
    }

    public void ActivateSpawners()
    {
        if (isBossRoom)
        {
            SpawnBoss();
            return;
        }
        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        List<GameObject> enemyTypes = Resources.LoadAll("Prefabs/Enemies").Cast<GameObject>().ToList();
        int[] dirtySpawners = new int[spawners.Count];
        int selectedSpawner;
        int selectedEnemy;

        for (int i = numEnemies; i < maxEnemies; i++)
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
                .SpawnEnemy(enemyTypes[selectedEnemy], enemies, gameObject);
            numEnemies++;
        }

        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<EnemySpawner>().currentEnemyCount = 0;
        }

        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().LoadStyles();
        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveIncomplete();
        UpdateObjective();
    }

    private void SpawnBoss()
    {
        GameObject bossType = Resources.Load<GameObject>("Prefabs/Bosses/TestBoss");

        bossSpawner.GetComponent<EnemySpawner>().SpawnEnemy(bossType, enemies, gameObject);
        numEnemies++;

        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().LoadStyles();
        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveIncomplete();
        UpdateObjective();

        StartCoroutine(SpawnMinions());
    }

    IEnumerator SpawnMinions()
    {
        yield return new WaitForSeconds(10);

        SpawnEnemies();
    }
    
    public void UpdateObjective()
    {
        string objectiveText = "Defeat Enemies\n" + (maxEnemies - enemies.Count) + " / " + maxEnemies;
        GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveText(objectiveText);
        if (enemies.Count == 0)
        {
            GetComponentInChildren<LevelExit>().canExit = true;
            GetComponentInChildren<LevelExit>().promptMessage = "Exit Level";
            GameObject.Find("Objective").GetComponent<ObjectiveHandler>().SetObjectiveComplete();
        }
    }
}
