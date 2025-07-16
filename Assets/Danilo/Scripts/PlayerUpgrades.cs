using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades Instance;

    public int fasterLevel = 0;
    public int piercingLevel = 0;
    public int explodingLevel = 0;

    //make sure that only one instance exists
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    //increase the level of the upgrade based on the type of upgrade
    public void UnlockUpgrade(Upgrades.UpgradeType type)
    {
        switch (type)
        {
            case Upgrades.UpgradeType.Faster:
                fasterLevel++;
                Debug.Log("Faster upgraded to level " + fasterLevel);
                break;
            case Upgrades.UpgradeType.Piercing:
                piercingLevel++;
                Debug.Log("Piercing upgraded to level " + piercingLevel);
                break;
            case Upgrades.UpgradeType.Exploding:
                explodingLevel++;
                Debug.Log("Exploding upgraded to level " + explodingLevel);
                break;
        }
    }

    //return the current level of the specific type of upgrade
    public int GetUpgradeLevel(Upgrades.UpgradeType type)
    {
        if (type == Upgrades.UpgradeType.Faster)
        {
            return fasterLevel;
        }
        else if (type == Upgrades.UpgradeType.Piercing)
        {
            return piercingLevel;
        }
        else if (type == Upgrades.UpgradeType.Exploding)
        {
            return explodingLevel;
        }
        else
        {
            return 0; 
        }
    }
}
