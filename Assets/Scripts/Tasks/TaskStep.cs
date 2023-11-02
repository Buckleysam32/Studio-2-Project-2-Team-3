using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;
    private string taskId;

    public void InitialiseTaskStep(string taskId)
    {
        this.taskId = taskId;
    }

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.instance.taskEvents.AdvanceTask(taskId);
            Destroy(this.gameObject);
        }
    }


}
