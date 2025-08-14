using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseConditions : MonoBehaviour
{
    
    public int coinsRequired = 10;
    public GameObject winPanel;
    public GameObject losePanel;

    private bool triggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; 

        if (other.CompareTag("Player"))
        {
            triggered = true;

            
            int playerCoins = ScoreManager.instance.GetCoinCount();

            if (playerCoins >= coinsRequired)
            {
                ShowWinPanel();
            }
            else
            {
                ShowLosePanel();
            }
        }
    }

    void ShowWinPanel()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

       
        DisablePlayerControl();
        FindAnyObjectByType<KillBox>().gameObject.SetActive(false);
    }

    public void ShowLosePanel()
    {
        if (losePanel != null)
            losePanel.SetActive(true);

        
        DisablePlayerControl();
    }

    void DisablePlayerControl()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = false;
        }
    }
}
