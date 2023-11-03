using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EndPoint : MonoBehaviour
{
    // this is the collider for the delivery point
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //change this to enabling the option to deliver upon submit being pressed
            GameEventsManager.instance.taskEvents.PackageDelivery();
        }
    }
}
