using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public PackageType packageType;
    public float durability = 100f;
    public int moneyReward = 0;
    public float timeReward = 0f;

    public float moneyModifier = 1f;
    public float timeModifier = 1f;

    private void Start()
    {
        RandomisePackageType();
        InitPackageValues();
    }

    private void RandomisePackageType()
    {
        int count = System.Enum.GetNames(typeof(PackageType)).Length;
        int randomCount = Random.Range(0 , count +1);

        switch (randomCount)
        {
            case 0:
                packageType = PackageType.normal;
                break;
            case 1:
                packageType = PackageType.fragile;
                break;
            case 2:
                packageType = PackageType.big;
                break;
        }
    }

    private void InitPackageValues()
    {
        moneyModifier = 1f;
        timeModifier = 1f; 


        switch (packageType)
        {
            case PackageType.normal:
                moneyReward = 100;
                timeReward = 15f;
                break;
            case PackageType.fragile:
                moneyReward = 200;
                timeReward = 15f;
                break;
            case PackageType.big:
                moneyReward = 100;
                timeReward = 30f;
                break;
        }
    }

    /// <summary>
    /// Reduces the durability of the package by in the input amount
    /// </summary>
    /// <param name="damage"></param>
    protected virtual void TakeDamage(float damage)
    {
        switch (packageType)
        {
            case PackageType.normal:
                durability -= damage;
                break;
            case PackageType.fragile:
                durability -= 1.5f*damage;
                break;
            case PackageType.big:
                durability -= 0.75f*damage;
                break;
        }

        if (durability <= 0)
        {
            DestroyPackage();
        }
    }

    /// <summary>
    /// Lowers the rewards given upon delivery
    /// </summary>
    protected virtual void DestroyPackage()
    {
        // lower the rewards
        moneyModifier = 0.75f;
        timeModifier = 0.5f;
    }

    public int GetMoneyReward()
    {
        return (int)(moneyReward * moneyModifier);
    }

    public float GetTimeReward()
    {
        return timeReward * timeModifier;
    }
}

public enum PackageType
{
    normal,
    fragile,
    big
}