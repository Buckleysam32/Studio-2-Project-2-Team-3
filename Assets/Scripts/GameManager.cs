using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Money")]
    public int currentMoney = 0;
    public int continueCost = 500;
    public float continueTimeGain = 60f; // how much time you get when continuing 

    [Header("Timer")]
    public float gameTimeLength = 180f; //the lenght of the game in seconds 
    public float currentSeconds;
    private Coroutine timer;
    public TMP_Text timerText;

    private void OnEnable()
    {
        //GameEventsManager.instance.gameEvents.onGameStart += StartTimer;
        //GameEventsManager.instance.gameEvents.onGameOver += GameOver;
        //GameEventsManager.instance.gameEvents.onTimerEnd += ContinuePrompt;
        //GameEventsManager.instance.gameEvents.onContinueGame += ContinueGame;
    }

    private void OnDisable()
    {
        //GameEventsManager.instance.gameEvents.onGameStart -= StartTimer;
        //GameEventsManager.instance.gameEvents.onGameOver -= GameOver;
        //GameEventsManager.instance.gameEvents.onTimerEnd -= ContinuePrompt;
        //GameEventsManager.instance.gameEvents.onContinueGame -= ContinueGame;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.0f; // just incase we didnt unpause from pause menu or from game overscreen

        //GameEventsManager.instance.gameEvents.GameStart();
        StartTimer(gameTimeLength); // can delete this once game events is set up
    }

    #region Timer
    // this region can be moved to the reward manager as that should have the money & time related functions


    /// <summary>
    /// Starts the timer if one doesn't already exist
    /// </summary>
    /// <param name="totalSeconds"></param>
    private void StartTimer(float totalSeconds)
    {
        if (timer == null)
        {
            timer = StartCoroutine(Timer(gameTimeLength));
        }
    }

    /// <summary>
    /// updates the UI timer text
    /// </summary>
    /// <param name="totalSeconds"></param>
    private void UpdateTimer(float totalSeconds)
    {
        // this could be moved to the UI manager script

        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 0)
        {
            seconds = 0;
            minutes += 1;
        }

        timerText.text = "Timer: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public IEnumerator Timer(float timerStartValue = 180)
    {
        currentSeconds = timerStartValue;

        while (currentSeconds > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentSeconds--;
            UpdateTimer(currentSeconds);
        }

        //GameEventsManager.instance.gameEvents.TimerEnd();
        Debug.Log("Game Over");
        yield return null;
    }
    #endregion

    #region Game Over
    private void ContinuePrompt()
    {
        // pause time
        Time.timeScale = 0;

        // check if player has enough money to keep playing
        if (currentMoney >= continueCost)
        {
            //enable continue button

        }

        //enable the Game Over panel
    }

    /// <summary>
    /// called to continue the game
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1;
        currentMoney -= continueCost;
        currentSeconds += continueTimeGain;
    }

    private void GameOver()
    {
        //do Game Over stuff here

        //set highscore
        if (currentMoney > PlayerPrefs.GetInt("Money"))
        {
            PlayerPrefs.SetInt("Money", currentMoney);
        }
        
        // return to main menu
        //GameEventsManager.instance.gameEvents.LoadScene(0);
    }

    #endregion
}
