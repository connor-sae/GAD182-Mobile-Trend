using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    public int penaltyValue = 1;

    //if the player triggers an obstacle then decrease the player's score
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-penaltyValue); 
            }
            else
            {
                Debug.LogWarning("ScoreManager instance not found when hitting obstacle.");
            }

            Destroy(gameObject);
        }
    }
}
