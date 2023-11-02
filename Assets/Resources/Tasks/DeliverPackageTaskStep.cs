using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPackageTaskStep : TaskStep
{
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
    public void DeliverPackage(string id)
    {
        if (packagesDelivered < amountToDeliver)
        {
            packagesDelivered++;
        }

        if (packagesDelivered >= amountToDeliver)
        {
            FinishTaskStep();
        }
    }
}
