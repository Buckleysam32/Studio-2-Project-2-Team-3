using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;
    private string taskId;
    private int stepIndex;

    public void InitialiseTaskStep(string taskId, int stepIndex, string taskStepState)
    {
        this.taskId = taskId;
        this.stepIndex = stepIndex;
        if (taskStepState != null && taskStepState != " ")
        {
            SetTaskStepState(taskStepState);
        }
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

    protected void ChangeState(string newState)
    {
        GameEventsManager.instance.taskEvents.TaskStepStateChange(taskId, stepIndex, new TaskStepState(newState));
    }

    protected abstract void SetTaskStepState(string state);

}