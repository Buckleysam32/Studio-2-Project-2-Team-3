using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class StartPoint : MonoBehaviour
{
    public bool hasPackage = true;

    // this is the collider for the start point
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasPackage = true;
        }
    }
}
