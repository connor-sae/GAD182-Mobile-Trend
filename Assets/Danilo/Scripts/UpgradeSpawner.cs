using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject fasterUpgradePrefab;
    public GameObject piercingUpgradePrefab;
    public GameObject explodingUpgradePrefab;

    public Transform spawnPoint;

    private GameObject currentUpgrade;

    void Start()
    {
        SpawnRandomUpgrade();
    }

    public void SpawnRandomUpgrade()
    {
        //randomly choose one of the three upgrades
        int choice = Random.Range(0, 3);
        GameObject selectedPrefab = null;

        switch (choice)
        {
            case 0:
                selectedPrefab = fasterUpgradePrefab;
                break;
            case 1:
                selectedPrefab = piercingUpgradePrefab;
                break;
            case 2:
                selectedPrefab = explodingUpgradePrefab;
                break;
        }

        //spawn the selected upgrade
        currentUpgrade = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

        //give this spawner a reference
        Upgrades upgradeScript = currentUpgrade.GetComponent<Upgrades>();
        if (upgradeScript != null)
        {
            upgradeScript.spawner = this;
        }
    }

    public void OnUpgradeDestroyed()
    {
        //wait a moment before spawning the next upgrade
        Invoke(nameof(SpawnRandomUpgrade), 1f);
    }
}
