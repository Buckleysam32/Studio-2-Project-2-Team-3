using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEvents
{
    public event Action<string> onTaskStart;

    /// <summary>
    /// called when the player starts a tasks
    /// </summary>
    public void StartTask(string id)
    {
        if (onTaskStart != null)
        {
            onTaskStart(id);
        }
    }

    public event Action<string> onPackageDelivery;

    public void PackageDelivery(string id)
    {
        if (onPackageDelivery != null)
        {
            onPackageDelivery(id);
        }
    }

    public event Action<string> onAdvanceTask;
    /// <summary>
    /// Called to advance to the next step in a task
    /// </summary>
    /// <param name="id"></param>
    public void AdvanceTask(string id)
    {
        if (onAdvanceTask != null)
        {
            onAdvanceTask(id);
        }
    }

    public event Action<string> onTaskCompleted;

    /// <summary>
    /// called when the player completes a task
    /// </summary>
    public void CompleteTask(string id)
    {
        if (onTaskCompleted != null)
        {
            onTaskCompleted(id);
        }
    }

    public event Action<string> onTaskFailed;

    /// <summary>
    /// called when the player fails a task
    /// </summary>
    public void FailTask(string id)
    {
        if (onTaskFailed != null)
        {
            onTaskFailed(id);
        }
    }

    public event Action<Task> onTaskStateChange;
    /// <summary>
    /// called when the state of a task changes
    /// </summary>
    /// <param name="task"></param>
    public void TaskStateChange(Task task)
    {
        if (onTaskStateChange != null)
        {
            onTaskStateChange(task);
        }
    }
}
