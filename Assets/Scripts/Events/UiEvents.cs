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

    public Action<GameObject> onActivateIndicator;

    public void ActivateIndicator(GameObject targetGameObject)
    {
        if (onActivateIndicator != null)
        {
            onActivateIndicator(targetGameObject);
        }
    }
}
