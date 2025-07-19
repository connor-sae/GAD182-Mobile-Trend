using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseManager : MonoBehaviour
{
    public float timeToSurvive = 15f; // Total time to survive
    private bool gameEnded = false;   // To prevent multiple endings

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI timerText;

    private PlayerHealth playerHealth;

    void Start()
    {
        // Find the player and get the PlayerHealth script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        // Hide both win/lose screens at the start
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        // Decrease the survival timer
        timeToSurvive -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Ceil(timeToSurvive).ToString();

        // Check if the player is dead
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            GameOver(false);
        }
        // Check if time has run out and player is alive
        else if (timeToSurvive <= 0)
        {
            GameOver(true);
        }
    }

    void GameOver(bool didWin)
    {
        gameEnded = true;

        if (didWin)
        {
            winScreen.SetActive(true);
            Debug.Log("You survived! You win!");
        }
        else
        {
            loseScreen.SetActive(true);
            Debug.Log("You died! Game over.");
        }

        // Optional: Freeze the game
        Time.timeScale = 0f;
    }
}
