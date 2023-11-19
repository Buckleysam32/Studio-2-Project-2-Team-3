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

    public Action<int> onPickUpPackage;

    public void PickUpPackage(int packageMaxHealth)
    {
        if (onPickUpPackage != null)
        {
            onPickUpPackage(packageMaxHealth);
        }
    }

    public Action<int> onPackageDamaged;

    public void PackageDamaged(int packageCurrentHealth)
    {
        if (onPickUpPackage != null)
        {
            onPackageDamaged(packageCurrentHealth);
        }
    }
}
