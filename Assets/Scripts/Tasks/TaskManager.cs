using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    // task start requirements
    private int numberOfTasks; // how many tasks we currently have active, this can later be an array of tasks we check the size of to find out
    // there may be some other requirements later
    
    private Dictionary<string, Task> taskMap;

    private void Awake()
    {
        taskMap =  CreateTaskMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onTaskStart += StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onTaskCompleted += CompleteTask;
        GameEventsManager.instance.taskEvents.onTaskFailed += FailTask;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStart -= StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onTaskCompleted -= CompleteTask;
        GameEventsManager.instance.taskEvents.onTaskFailed -= FailTask;
    }



    private void Start()
    {
        //broadcast the initial state of all quests on startup
        foreach (Task task in taskMap.Values)
        {
            GameEventsManager.instance.taskEvents.TaskStateChange(task);
        }
    }

    private void Update()
    {
        // loop through ALL tasks
        foreach (Task task in taskMap.Values)
        {
            // if we're meeting the requirements, switch over to the can start state
            if (task.state == TaskState.RequirementsNotMet && CheckRequirementsMet(task))
            {
                ChangeTaskState(task.info.id, TaskState.CanStart);
            }
        }
    }

    private Dictionary<string, Task> CreateTaskMap()
    {
        // Loads all TaskInfoSO scriptable objects under the Assets/Resources/Tasks folder
        TaskInfoSO[] allTasks = Resources.LoadAll<TaskInfoSO>("Tasks");
        // create the taskmap
        Dictionary<string, Task> idToTaskMap = new Dictionary<string, Task>();
        foreach (TaskInfoSO taskInfo in allTasks)
        {
            if (idToTaskMap.ContainsKey(taskInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating task map: " + taskInfo.id);
            }
            idToTaskMap.Add(taskInfo.id, new Task(taskInfo));
        }
        return idToTaskMap;
    }

    private Task GetTaskById(string id)
    {
        Task task = taskMap[id];
        if (task == null)
        {
            Debug.LogError("ID not found in the task map: " + id);
        }
        return task;
    }

    private void ChangeTaskState(string id, TaskState state)
    {
        Task task = GetTaskById(id);
        task.state = state;
        GameEventsManager.instance.taskEvents.TaskStateChange(task);
    }

    private bool CheckRequirementsMet(Task task)
    {
        // start true and prove to be false

        bool meetsRequirements = true;

        //check if we have less than our maximum number of tasks
        if (numberOfTasks >= task.info.maximumTasks)
        {
            meetsRequirements = false;
        }

        //check for task prerequisites for completion
        foreach (TaskInfoSO prerequisiteTaskInfo in task.info.taskPrerequisites)
        {
            if (GetTaskById(prerequisiteTaskInfo.id).state != TaskState.Finished)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void StartTask(string id)
    {
        numberOfTasks++; // this can be removed later when the number of tasks can be found from its array size
        Task task = GetTaskById(id);
        task.InstantiateCurrentQuestStep(this.transform);
        ChangeTaskState(task.info.id, TaskState.InProgress);

        Debug.Log("Start Task: " + id);
    }

    private void AdvanceTask(string id)
    {
        Task task = GetTaskById(id);

        // move on to the next step
        task.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (task.CurrentStepExist())
        {
            task.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for the task
        else
        {
            ChangeTaskState(task.info.id, TaskState.CanFinish);
        }

        Debug.Log("Advance Task: " + id);
    }

    private void CompleteTask(string id)
    {
        Task task = GetTaskById(id);
        ClaimRewards(task);
        ChangeTaskState(task.info.id, TaskState.Finished);
    }

    private void ClaimRewards(Task task)
    {
        GameEventsManager.instance.rewardEvents.MoneyGained(task.info.moneyReward);
        GameEventsManager.instance.rewardEvents.TimeGained(task.info.timeReward);
    }

    private void FailTask(string id)
    {
        // To Do: Fail the task
        Debug.Log("Fail Task: " + id);
    }
}
