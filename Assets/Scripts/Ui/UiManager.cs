using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject continuePanel;
    public GameObject pausePanel;

    public TMP_Text timerText;
    public TMP_Text moneyText;

    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate += UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange += UpdateMoney;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate -= UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange -= UpdateMoney;
    }

    // Start is called before the first frame update
    void Start()
    {
        continuePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// updates the UI timer text
    /// </summary>
    /// <param name="totalSeconds"></param>
    private void UpdateTimer(float totalSeconds)
    {    
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 0 && minutes != 0)
        {
            seconds = 0;
            minutes += 1;
        }

        timerText.text = "Timer: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void UpdateMoney(int money)
    {
        moneyText.text = "Money: $" + money;
    }
}
