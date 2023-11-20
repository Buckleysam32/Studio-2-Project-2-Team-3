using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitLocationTaskStep : TaskStep
{

    private void Start()
    {
        //GameEventsManager.instance.uiEvents.onActivateIndicator(this.gameObject);
        GameEventsManager.instance.uiEvents.SetStepInstructionText("Drive By Drop Off");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishTaskStep();
        }
    }


    protected override void SetTaskStepState(string state)
    {
        // no state is needed for this quest step
    }

}
