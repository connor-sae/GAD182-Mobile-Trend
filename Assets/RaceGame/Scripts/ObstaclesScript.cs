using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    public int penaltyValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

            Destroy(gameObject);
        }
    }
}
