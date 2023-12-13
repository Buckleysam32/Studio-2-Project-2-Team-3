using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    // static info
    public TaskInfoSO info;

    public bool taskActive; // has the task manager enabled this task so it can be started
    public TaskState state;
    public int currentTaskStepIndex;
    private TaskStepState[] taskStepStates;
    //public TargetIndicator indicater;

    public void Awake()
    {
        //indicater = GameObject.FindGameObjectWithTag("Indicator").GetComponent<TargetIndicator>();
    }

    //constructor for making a new task 
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

    // constructor for making a task from save data
    public Task(TaskInfoSO taskInfo,bool taskActive, TaskState taskState, int currentTaskStepIndex, TaskStepState[] taskStepStates)
    {
        this.info = taskInfo;
        this.taskActive = taskActive; 
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

    /// <summary>
    /// iterates the current task step index
    /// </summary>
    public void MoveToNextStep()
    {
        currentTaskStepIndex++;
    }

    /// <summary>
    /// Checks if the current task step index can fit in the task step prefab array
    /// </summary>
    /// <returns></returns>
    public bool CurrentStepExist()
    {
        return (currentTaskStepIndex < info.taskStepPrefabs.Length);
    }

    /// <summary>
    /// Instantiates the current task step as a child of the input transform
    /// </summary>
    /// <param name="parentTransform"></param>
    public void InstantiateCurrentTaskStep(Transform parentTransform)
    {
        GameObject taskStepPrefab = GetCurrentTaskStepPrefab();
        if (taskStepPrefab != null)
        {
            // could do object pooling here if performance is an issue
           TaskStep taskStep = UnityEngine.Object.Instantiate<GameObject>(taskStepPrefab, parentTransform).GetComponentInChildren<TaskStep>();
            //indicater.Target = taskStep.transform;
            // initialise the step state, incase we're loading data
            taskStep.InitialiseTaskStep(info.id, currentTaskStepIndex, taskStepStates[currentTaskStepIndex].state);
        }
    }

    /// <summary>
    /// Returns the prefab of the current task step, if it exists
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// sets the task step state of the input stepIndex to the input TaskStepState
    /// </summary>
    /// <param name="taskStepState"></param>
    /// <param name="stepIndex"></param>
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
        return new TaskData(taskActive,state, currentTaskStepIndex, taskStepStates);
    }
}
