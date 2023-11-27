using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [Header("Money")]
    public int currentMoney = 0;

    [Header("FeedbackUI")]
    public FeedbackUI moneyFeedbackUI;

    [Header("Timer")]
    public float gameTimeLength = 180f; //the length of the game in seconds 
    public float currentSeconds;
    private Coroutine timer;

    [Header("Continue")]
    public int continueCost = 500; // how much it costs to continue playing
    public float continueTimeGain = 60f; // how much time you get when continuing 

    /*
     *      string moneyText;
            moneyText = money.ToString();
            bool isPositive;
            bool displayType;
            if (money > 0)
            {
                isPositive = true;
                displayType = true;

            }
            else
            {
                isPositive = false;
                displayType = false;
            }
            //Call feedback
            feedbackUI.FeedbackStart(moneyText, isPositive, displayType);
     */


    private void OnEnable()
    {
        GameEventsManager.instance.rewardEvents.onMoneyGained += MoneyGained;
        GameEventsManager.instance.rewardEvents.onTimeGained += TimeGained;
        GameEventsManager.instance.gameEvents.onTimerStart += StartTimer;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.rewardEvents.onMoneyGained -= MoneyGained;
        GameEventsManager.instance.rewardEvents.onTimeGained -= TimeGained;
        GameEventsManager.instance.gameEvents.onTimerStart -= StartTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEventsManager.instance.rewardEvents.MoneyChange(currentMoney);
        GameEventsManager.instance.rewardEvents.TimeChange(currentSeconds);
    }

    private void MoneyGained(int money)
    {
        currentMoney += money;
        GameEventsManager.instance.rewardEvents.MoneyChange(currentMoney);
        string moneyText;
        moneyText = money.ToString();
        bool isPositive;
        bool displayType;
        if (money > 0)
        {
            isPositive = true;
            displayType = true;

        }
        else
        {
            isPositive = false;
            displayType = false;
        }
        //Call feedback
        if(moneyFeedbackUI != null)
        {
            moneyFeedbackUI.FeedbackStart(moneyText, isPositive, displayType);
        }


    }

    private void TimeGained(float time)
    {
        currentSeconds += time;
        GameEventsManager.instance.rewardEvents.TimeChange(currentSeconds);
    }

    #region Timer
    /// <summary>
    /// Starts the timer if one doesn't already exist
    /// </summary>
    /// <param name="totalSeconds"></param>
    private void StartTimer(float seconds)
    {
            timer = StartCoroutine(Timer(seconds));
    }

    public IEnumerator Timer(float timerStartValue = 180)
    {
        currentSeconds = timerStartValue;

        while (currentSeconds > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentSeconds--;
            GameEventsManager.instance.uiEvents.UpdateTimer(currentSeconds);
        }

        GameEventsManager.instance.gameEvents.TimerEnd();
        yield return null;
    }
    #endregion
}
