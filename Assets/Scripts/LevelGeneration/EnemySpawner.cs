using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 12.5f;
    public int maxEnemyCount;
    public int currentEnemyCount = 0;

    public void SpawnEnemy(GameObject enemyType, List<GameObject> enemies, GameObject room)
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        bool canSpawn = false;
        while (!canSpawn)
        {
            Vector2 v2 = Random.insideUnitCircle;
            Vector3 mod = new Vector3(v2.x, 0, v2.y);
            spawnPoint = transform.position + (mod * spawnRadius);
            Debug.Log(spawnPoint);
            Ray ray = new Ray(spawnPoint, Vector3.up);
            if (!Physics.Raycast(ray, 5))
                canSpawn = true;
        }

        enemies.Add(Instantiate(enemyType, spawnPoint, Quaternion.identity, room.transform));
        currentEnemyCount++;
    }
}
