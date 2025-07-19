using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;

    private float timer = 0f;

    //to check if the enemyspawner script is actually running
    void Start()
    {
        Debug.Log("EnemySpawner is running!");
    }

    //if the timer has passed the spawn interval, spawn an enemy
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    
    void SpawnEnemy()
    {
        //checking if this function works
        Debug.Log("Trying to spawn enemy...");

        //if theres no enemyprefab
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab is not assigned!");
            return;
        }

        //if there isnt any spawn points
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        //choose a random spawnpoint from an array of spawnpoints
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //spawn an enemy at the spawnpoint
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemy spawned at: " + spawnPoint.position);
    }
}
