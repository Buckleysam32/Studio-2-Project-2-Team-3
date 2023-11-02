using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishTask()
    {
        if (!isFinished)
        {
            isFinished = true;

            // To do: advance the task forward now that we've finished this particular step

            Destroy(this.gameObject);
        }
    }
}
