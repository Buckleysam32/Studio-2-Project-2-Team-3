using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    public bool taskActive;
    public TaskState state;
    public int taskStepIndex;
    public TaskStepState[] taskStepStates;

    public TaskData(bool taskActive,TaskState state, int taskStepIndex, TaskStepState[] taskStepStates)
    {
        this.taskActive = taskActive;
        this.state = state;
        this.taskStepIndex = taskStepIndex;
        this.taskStepStates = taskStepStates;
    }   
}
