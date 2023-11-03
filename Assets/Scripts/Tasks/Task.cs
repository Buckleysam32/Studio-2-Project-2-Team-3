using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    // static info
    public TaskInfoSO info;

    // static info
    public TaskState state;
    private int currentTaskStepIndex;
    private TaskStepState[] taskStepStates;

    public Task(TaskInfoSO taskInfo)
    {
        this.info = taskInfo;
        this.state = TaskState.RequirementsNotMet;
        this.currentTaskStepIndex = 0;
        this.taskStepStates = new TaskStepState[info.taskStepPrefabs.Length];
        for (int i = 0; i < taskStepStates.Length; i++)
        {
            taskStepStates[i] = new TaskStepState();
        }
    }

    public Task(TaskInfoSO taskInfo, TaskState taskState, int currentTaskStepIndex, TaskStepState[] taskStepStates)
    {
        this.info = taskInfo;
        this.state = taskState;
        this.currentTaskStepIndex = currentTaskStepIndex;
        this.taskStepStates = taskStepStates;

        //if the task step states and prefabs are different lenghts,
        // something has changed during development and the saved data is out of sync
        if (this.taskStepStates.Length != this.info.taskStepPrefabs.Length)
        {
            Debug.LogWarning("Task Step Prefabs and Task Step States are "
                + "of different lengths. this indicates something changed "
                + "with TaskInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. TaskId: " + this.info.id);
        }
    }

    public void MoveToNextStep()
    {
        currentTaskStepIndex++;
    }

    public bool CurrentStepExist()
    {
        return (currentTaskStepIndex < info.taskStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject taskStepPrefab = GetCurrentTaskStepPrefab();
        if (taskStepPrefab != null)
        {
            // could do object pooling here if performance is an issue
           TaskStep taskStep = UnityEngine.Object.Instantiate<GameObject>(taskStepPrefab, parentTransform).GetComponent<TaskStep>();
            
            taskStep.InitialiseTaskStep(info.id, currentTaskStepIndex, taskStepStates[currentTaskStepIndex].state);
        }
    }

    private GameObject GetCurrentTaskStepPrefab()
    {
        GameObject taskStepPrefab = null;
        if (CurrentStepExist())
        {
            taskStepPrefab = info.taskStepPrefabs[currentTaskStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but the stepIndex was out of range indicating that " 
                + "there's no current step: TaskId = " +info.id 
                + " ,stepIndex = " + currentTaskStepIndex);
        }
        return taskStepPrefab;
    }

    public void StoreTaskStepState(TaskStepState taskStepState, int stepIndex)
    {
        if (stepIndex < taskStepStates.Length)
        {
            taskStepStates[stepIndex].state = taskStepState.state;
        }
        else
        {
            Debug.LogWarning("Tried to access task step data, but stepIndex was out of range: " 
                + "TaskId = " + info.id + " Step Index = " + stepIndex);
        }
    }

    public TaskData GetTaskData()
    {
        return new TaskData(state, currentTaskStepIndex, taskStepStates);
    }
}
