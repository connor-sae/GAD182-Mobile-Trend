using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] float gameOverScreenDelay = 3f;
    [SerializeField] private State[] progressionStates;

    private State[] availableStates;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }

        availableStates = progressionStates;
    }
    public bool gameOver;
    public void PinPulled(int ID)
    {

        bool stateFound = false;
        foreach (State state in availableStates)
        {
            if(state.pinTriggerID == ID)
            {
                stateFound = true;
                state.onStateEnter?.Invoke();
                if (!(state.stateAnimationTrigger == ""))
                    playerAnimator.SetTrigger(state.stateAnimationTrigger);

                switch(state.progressState)
                {
                    case StateProgressor.CONTINUE:

                        availableStates = state.continuedStates;
                        break;
                    case StateProgressor.VICTORY:

                        OnGameWin();
                        break;
                    case StateProgressor.LOSE:

                        OnGameLose();
                        break;
                }

                break;
            }

        }
        if (!stateFound)
        {
            Debug.LogError($"No available state with the ID {ID} found");
            //return;
        }
    }

    public void OnGameWin()
    {
        Debug.Log("Game Win");
        gameOver = true;
    }

    public void OnGameLose()
    {
        Debug.Log("Game Lose");
        gameOver = true;
    }

    IEnumerator ShowGameOverScreen(float delay, bool won)
    {
        yield return new WaitForSeconds(delay);
        if (won)
            winScreen.SetActive(true);
        else
            loseScreen.SetActive(true);
    }

    [Serializable]
    private class State
    {
        public int pinTriggerID;
        public UnityEvent onStateEnter;
        public string stateAnimationTrigger;
        public StateProgressor progressState;
        public State[] continuedStates;
    }

}

public enum StateProgressor
{ 
    CONTINUE,
    VICTORY,
    //DIE,
    LOSE,
}


