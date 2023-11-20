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

    private void GameStart()
    {
        Time.timeScale = 1.0f; // just incase we didnt unpause from pause menu or from game overscreen      
        startRoutine = StartCoroutine(StartRoutine());
        
    }

    IEnumerator StartRoutine()
    {
        // take away movement from player
        float tempMaxSpeed = playerController.maxSpeed;
        playerController.maxSpeed = 0;
        //show instructions
        uiManager.instructionPanel.SetActive(true);

        yield return new WaitForSeconds(5);

        // get rid of instructions
        uiManager.instructionPanel.SetActive(false);
        // turn on game information UI
        uiManager.gameInformation.SetActive(true);
        // reset max speed
        playerController.maxSpeed = tempMaxSpeed;
        //start timer
        GameEventsManager.instance.gameEvents.TimerStart(rewardManager.currentSeconds);
        uiManager.miniMap.SetActive(true);

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
        Time.timeScale = 1;
        isGameOverScreen = false;
        // need to check if the player has enough money before deducting
        GameEventsManager.instance.rewardEvents.MoneyGained(-rewardManager.continueCost);
        GameEventsManager.instance.rewardEvents.TimeGained(rewardManager.continueTimeGain);
        GameEventsManager.instance.gameEvents.TimerStart(rewardManager.currentSeconds);
        uiManager.continuePanel.SetActive(false);
    }

    private void GameOver()
    {
        //do Game Over stuff here

        //set highscore
        GameEventsManager.instance.gameEvents.SetHighScore(rewardManager.currentMoney);
        
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
