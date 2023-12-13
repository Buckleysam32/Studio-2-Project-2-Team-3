using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EndPoint : MonoBehaviour
{
    public TargetIndicator indicater;

    public void Awake()
    {
        indicater = GameObject.FindGameObjectWithTag("Indicator").GetComponent<TargetIndicator>();
    }

    public void Start()
    {
        indicater.Target = this.transform;
    }

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
