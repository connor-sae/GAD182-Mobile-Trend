using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    public int penaltyValue = 1;

    // If you want this to be a trigger instead of a physical collision, check "Is Trigger" on the collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Subtract score if ScoreManager exists
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-penaltyValue); // negative to reduce
            }
            else
            {
                Debug.LogWarning("ScoreManager instance not found when hitting obstacle.");
            }
        }
    }
}
