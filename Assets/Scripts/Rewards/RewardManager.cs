using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public int currentMoney = 0; // how much money the player currently has
    public float currentTime = 30f; // how much game time is left

    private void OnEnable()
    {
        GameEventsManager.instance.rewardEvents.onMoneyGained += MoneyGained;
        GameEventsManager.instance.rewardEvents.onTimeGained += TimeGained;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.rewardEvents.onMoneyGained -= MoneyGained;
        GameEventsManager.instance.rewardEvents.onTimeGained -= TimeGained;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEventsManager.instance.rewardEvents.MoneyChange(currentMoney);
        GameEventsManager.instance.rewardEvents.TimeChange(currentTime);
    }

    private void MoneyGained(int money)
    {
        currentMoney += money;
        GameEventsManager.instance.rewardEvents.MoneyChange(currentMoney);
    }

    private void TimeGained(float time)
    {
        currentTime += time;
        GameEventsManager.instance.rewardEvents.TimeChange(currentTime);
    }
}
