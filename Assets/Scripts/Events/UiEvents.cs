using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEvents
{
    public Action<float> onTimerUpdate;

    public void UpdateTimer(float seconds)
    {
        if (onTimerUpdate != null)
        {
            onTimerUpdate(seconds);
        }
    }
}
