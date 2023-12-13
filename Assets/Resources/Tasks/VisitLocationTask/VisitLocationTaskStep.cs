using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitLocationTaskStep : TaskStep
{
    public TargetIndicator indicater;

    public void Awake()
    {
        indicater = GameObject.FindGameObjectWithTag("Indicator").GetComponent<TargetIndicator>();
    }

    private void Start()
    {
        //GameEventsManager.instance.uiEvents.onActivateIndicator(this.gameObject);
        GameEventsManager.instance.uiEvents.SetStepInstructionText("Drive By Drop Off");
        indicater.Target = this.transform;

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
