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

    public Task(TaskInfoSO taskInfo)
    {
        this.info = taskInfo;
        this.state = TaskState.RequirementsNotMet;
        this.currentTaskStepIndex = 0;
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
            
            taskStep.InitialiseTaskStep(info.id);
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
}
