using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCoinSpawner : MonoBehaviour
{
    
    public GameObject collectiblePrefab;
    public GameObject obstaclePrefab;
    public Transform spawnArea; 
    public int collectibleCount = 5;
    public int obstacleCount = 5;
    public float minSpacing = 1.0f; 

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        SpawnObjects(collectiblePrefab, collectibleCount);
        SpawnObjects(obstaclePrefab, obstacleCount);
    }

    // 
    void SpawnObjects(GameObject prefab, int count)
    {
        Bounds bounds = spawnArea.GetComponent<Collider>().bounds;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos;
            int attempts = 0;

            do
            {
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float z = Random.Range(bounds.min.z, bounds.max.z);
                float y = bounds.max.y;

                spawnPos = new Vector3(x, y, z);

                attempts++;

                if (attempts > 100) break;

            } while (!IsPositionValid(spawnPos));

            usedPositions.Add(spawnPos);
            Instantiate(prefab, spawnPos, Quaternion.Euler(0f, 90f, 0f));
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 usedPos in usedPositions)
        {
            if (Vector3.Distance(position, usedPos) < minSpacing)
            {
                return false;
            }
        }
        return true;
    }
}
