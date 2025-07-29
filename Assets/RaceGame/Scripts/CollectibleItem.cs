using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int scoreValue = 1; // How much score this item gives

    //if something with the player tag collides with the collectible item it adds to the score and destroys the game object
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
