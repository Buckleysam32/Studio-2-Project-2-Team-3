using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitLocationTaskStep : TaskStep
{
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
