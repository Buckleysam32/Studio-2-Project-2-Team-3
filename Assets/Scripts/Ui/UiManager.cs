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
    public GameObject stepInstruction;
    public GameObject miniMap;
    public GameObject fullMap;

    public bool mapActivated = false;

    public TMP_Text timerText;
    public TMP_Text moneyText;

    public GameObject healthBarGO;
    public GameObject basicPackage;
    public GameObject fragilePackage;
    public GameObject overSizePackage;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient gradient;

    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate += UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange += UpdateMoney;
        GameEventsManager.instance.uiEvents.onPickUpPackage += SetInitialHealth;
        GameEventsManager.instance.uiEvents.onPickUpPackage += SetPackageTypeIcon;
        GameEventsManager.instance.rewardEvents.onPlayerCrashed += UpdateHealth;
        GameEventsManager.instance.uiEvents.onSetStepInstructionText += SetStepText;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onTimerUpdate -= UpdateTimer;
        GameEventsManager.instance.rewardEvents.onMoneyChange -= UpdateMoney;
        GameEventsManager.instance.uiEvents.onPickUpPackage -= SetInitialHealth;
        GameEventsManager.instance.uiEvents.onPickUpPackage -= SetPackageTypeIcon;
        GameEventsManager.instance.rewardEvents.onPlayerCrashed -= UpdateHealth;
        GameEventsManager.instance.uiEvents.onSetStepInstructionText -= SetStepText;
    }

    // Start is called before the first frame update
    void Start()
    {
        continuePanel.SetActive(false);
        pausePanel.SetActive(false);
        instructionPanel.SetActive(false);
        gameInformation.SetActive(false);

        healthBarGO.SetActive(false);
        basicPackage.SetActive(false);
        fragilePackage.SetActive(false);
        overSizePackage.SetActive(false);

        fullMap.SetActive(false);
        miniMap.SetActive(false);
    }

    private void Update()
    {
        ControlMap();

    }

    public void ControlMap()
    {
        if (Input.GetKeyDown(KeyCode.M) && mapActivated == false)
        {
            Debug.Log("Open Map");

            Time.timeScale = 0;

            miniMap.SetActive(false);

            fullMap.SetActive(true);

            mapActivated = true;

        }
        else if (Input.GetKeyDown(KeyCode.M) && mapActivated == true)
        {
            Debug.Log("Close Map");

            Time.timeScale = 1;

            miniMap.SetActive(true);

            fullMap.SetActive(false);

            mapActivated = false;

        }
    }

    /// <summary>
    /// updates the UI timer text
    /// </summary>
    /// <param name="totalSeconds"></param>
    private void UpdateTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        timerText.text = "Timer: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void UpdateMoney(int money)
    {
        moneyText.text = "Money: $" + money;
    }

    public void SetInitialHealth(int maxHealth)
    {
        healthBar.maxValue = 100f;
        healthBar.value = 100f;
        healthFill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(float damage)
    {
        healthBar.value -= damage;
        healthFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SetPackageTypeIcon(int packageType)
    {
        switch (packageType)
        {
            case 0:
                healthBarGO.SetActive(true);
                basicPackage.SetActive(true);
                fragilePackage.SetActive(false);
                overSizePackage.SetActive(false);
                break;
            case 1:
                healthBarGO.SetActive(true);
                basicPackage.SetActive(false);
                fragilePackage.SetActive(true);
                overSizePackage.SetActive(false);
                break;
            case 2:
                healthBarGO.SetActive(true);
                basicPackage.SetActive(false);
                fragilePackage.SetActive(false);
                overSizePackage.SetActive(true);
                break;
            case 3:
                Debug.Log("Turning off package icon ui");
                healthBarGO.SetActive(false);
                basicPackage.SetActive(false);
                fragilePackage.SetActive(false);
                overSizePackage.SetActive(false);
                break;
        }
    }

    private void SetStepText(string text)
    {
        stepInstruction.GetComponent<TMP_Text>().text = text;
    }
}
