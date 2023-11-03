using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DeliverPackageTaskStep : TaskStep
{
    //the transform on the DeliverPackageTask Prefab will be the delivery location

    private int amountToDeliver = 2;
    private int packagesDelivered = 0;

    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onPackageDelivery += DeliverPackage;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onPackageDelivery -= DeliverPackage;
    }

    /// <summary>
    /// called to deliver the package and finish the task step
    /// </summary>
    public void DeliverPackage()
    {
        if (packagesDelivered < amountToDeliver)
        {
            packagesDelivered++;
            UpdateState();
        }

        if (packagesDelivered >= amountToDeliver)
        {
            FinishTaskStep();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //change this to enabling the option to deliver upon submit being pressed
            GameEventsManager.instance.taskEvents.PackageDelivery();
        }
    }


    private void UpdateState()
    {
        string state = packagesDelivered.ToString();
        ChangeState(state);
    }

    protected override void SetTaskStepState(string state)
    {
        // convert our state string back into an integer
        this.packagesDelivered = System.Int32.Parse(state);
        UpdateState();
    }
}
