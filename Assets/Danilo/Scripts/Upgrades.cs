using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public enum UpgradeType { Faster, Piercing, Exploding }
    public UpgradeType upgradeType;
    public int health = 20;

    public UpgradeSpawner spawner;

    //the gameobject of the upgrades take damage and if it's health is less than 0 apply the upgrade to the player
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} upgrade took {amount} damage. Remaining: {health}");

        if (health <= 0)
        {
            ApplyUpgrade();
        }
    }

    //apply the upgrade to the player and destroy the game object
    void ApplyUpgrade()
    {
        PlayerUpgrades.Instance.UnlockUpgrade(upgradeType);

        if (spawner != null)
        {
            spawner.OnUpgradeDestroyed();
        }

        Destroy(gameObject);
    }
}
