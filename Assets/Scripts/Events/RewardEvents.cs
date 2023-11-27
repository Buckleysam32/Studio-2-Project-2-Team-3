using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEvents
{
    public event Action<int> onMoneyGained;

    public void MoneyGained(int money)
    {
        if (onMoneyGained != null)
        {
            onMoneyGained(money);
        }
    }

    public event Action<int> onMoneyChange;

    public void MoneyChange(int money)
    {
        if (onMoneyChange != null)
        {
            onMoneyChange(money);
        }
    }

    public event Action<float> onTimeGained;

    public void TimeGained(float time)
    {
        if (onTimeGained != null)
        {
            onTimeGained(time);
        }
    }

    public event Action<float> onTimeChange;

    public void TimeChange(float time)
    {
        if (onTimeChange != null)
        {
            onTimeChange(time);
        }
    }
}
