using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public float durability = 100f;



    /// <summary>
    /// Reduces the durability of the package by in the input amount
    /// </summary>
    /// <param name="damage"></param>
    protected virtual void TakeDamage(float damage)
    {
        durability -= damage;
    }
}
