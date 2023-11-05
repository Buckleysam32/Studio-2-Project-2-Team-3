using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RewardManager rewardManager;
    public UiManager uiManager;

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
        Time.timeScale = 1.0f; // just incase we didnt unpause from pause menu or from game overscreen

        GameEventsManager.instance.gameEvents.GameStart();
    }

    private void GameStart()
    {
        //maybe wait for a few seconds
        // do a count down?
        // then start the timer
        GameEventsManager.instance.gameEvents.TimerStart(rewardManager.currentSeconds);
    }

    #region Game Over
    private void ContinuePrompt()
    {
        // pause time
        Time.timeScale = 0;

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
        rewardManager.currentMoney -= rewardManager.continueCost;
        rewardManager.currentSeconds += rewardManager.continueTimeGain;
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
