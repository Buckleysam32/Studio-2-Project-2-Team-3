using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RewardManager rewardManager;
    public UiManager uiManager;
    private Coroutine startRoutine;
    public PlayerController playerController;
    public bool isGameOverScreen;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onGameStart += GameStart;
        GameEventsManager.instance.gameEvents.onGameOver += GameOver;
        GameEventsManager.instance.gameEvents.onTimerEnd += ContinuePrompt;
        GameEventsManager.instance.gameEvents.onContinueGame += ContinueGame;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onGameStart -= GameStart;
        GameEventsManager.instance.gameEvents.onGameOver -= GameOver;
        GameEventsManager.instance.gameEvents.onTimerEnd -= ContinuePrompt;
        GameEventsManager.instance.gameEvents.onContinueGame -= ContinueGame;
    }

    // Start is called before the first frame update
    private void Start()
    {     
        GameEventsManager.instance.gameEvents.GameStart();
    }

    public void CloseInstructionPanel()
    {
        uiManager.instructionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void GameStart()
    {
        Time.timeScale = 1.0f; // just incase we didnt unpause from pause menu or from game overscreen      
        startRoutine = StartCoroutine(StartRoutine());
        
    }

    IEnumerator StartRoutine()
    {
        // play background music
        GameEventsManager.instance.audioEvents.Play("Mus_OpenTrip");
        // take away movement from player
        float tempMaxSpeed = playerController.maxSpeed;
        playerController.maxSpeed = 0;
        //show instructions
        uiManager.instructionPanel.SetActive(true);
        // turn on game information UI
        uiManager.gameInformation.SetActive(true);
        // reset max speed
        playerController.maxSpeed = tempMaxSpeed;
        //start timer
        GameEventsManager.instance.gameEvents.TimerStart(rewardManager.currentSeconds);
        uiManager.miniMap.SetActive(true);

        Time.timeScale = 0;

        yield return null;
    }


    #region Game Over
    private void ContinuePrompt()
    {
        // pause time
        Time.timeScale = 0;

        isGameOverScreen = true;

        // check if player has enough money to keep playing
        if (rewardManager.currentMoney >= rewardManager.continueCost)
        {
            //enable continue button

        }

        //enable the continue prompt panel
        uiManager.continuePanel.SetActive(true);
    }

    /// <summary>
    /// called to continue the game
    /// </summary>
    private void ContinueGame()
    {
        
        // need to check if the player has enough money before deducting
        if (rewardManager.currentMoney >= rewardManager.continueCost)
        {
            // we have enough money
            Time.timeScale = 1; //unfreeze time
            isGameOverScreen = false;

            // deduct the money cost
            GameEventsManager.instance.rewardEvents.MoneyGained(-rewardManager.continueCost);
            // give them some extra time
            GameEventsManager.instance.rewardEvents.TimeGained(rewardManager.continueTimeGain);
            //restart the timer
            GameEventsManager.instance.gameEvents.TimerStart(rewardManager.currentSeconds);
            // turn off the continue panel
            uiManager.continuePanel.SetActive(false);
        }
        else
        {
            //we dont have enough money to continue
            uiManager.insufficientFundsText.SetActive(true);
        }

    }

    private void GameOver()
    {
        //do Game Over stuff here

        //set highscore
        GameEventsManager.instance.gameEvents.SetHighScore(rewardManager.currentMoney);
        Time.timeScale = 1; //unfreeze time
        // return to main menu
        GameEventsManager.instance.gameEvents.LoadScene(0);
    }
    #endregion

    #region Continue Prompt Button Functions
    //these should go in the UI manager 
    public void Submit()
    {
        GameEventsManager.instance.gameEvents.GameOver();
    }

    public void Continue()
    {
        GameEventsManager.instance.gameEvents.ContinueGame();
    }
    #endregion
}
