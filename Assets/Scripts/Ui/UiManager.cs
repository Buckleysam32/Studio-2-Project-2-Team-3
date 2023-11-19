using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject continuePanel;
    public GameObject pausePanel;
    public GameObject instructionPanel;
    public GameObject gameInformation;

    public TMP_Text timerText;
    public TMP_Text moneyText;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient gradient;

    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate += UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange += UpdateMoney;
        GameEventsManager.instance.uiEvents.onPickUpPackage += SetInitialHealth;
        GameEventsManager.instance.uiEvents.onPackageDamaged += UpdateHealth;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate -= UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange -= UpdateMoney;
        GameEventsManager.instance.uiEvents.onPickUpPackage -= SetInitialHealth;
        GameEventsManager.instance.uiEvents.onPackageDamaged -= UpdateHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        continuePanel.SetActive(false);
        pausePanel.SetActive(false);
        instructionPanel.SetActive(false);
        gameInformation.SetActive(false);
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

    public void SetInitialHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        healthFill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(int currentHealth)
    {
        healthBar.value = currentHealth;
        healthFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}
