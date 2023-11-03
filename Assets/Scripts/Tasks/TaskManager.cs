using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadTaskState = true;

    private Dictionary<string, Task> taskMap;

    // task start requirements
    private int numberOfTasks; // how many tasks we currently have active, this can later be an array of tasks we check the size of to find out

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
        GameEventsManager.instance.taskEvents.onTaskStepStateChange += TaskStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStart -= StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onTaskCompleted -= CompleteTask;
        GameEventsManager.instance.taskEvents.onTaskFailed -= FailTask;
        GameEventsManager.instance.taskEvents.onTaskStepStateChange -= TaskStepStateChange;
    }



    private void Start()
    {
        foreach (Task task in taskMap.Values)
        {
            // initialize any loaded task steps
            if (task.state == TaskState.InProgress)
            {
                task.InstantiateCurrentQuestStep(this.transform);
            }

            //broadcast the initial state of all quests on startup
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
            idToTaskMap.Add(taskInfo.id, LoadTask(taskInfo));
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

    /// <summary>
    /// Checks the requirements to start a new task
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
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

    private void TaskStepStateChange(string id, int stepIndex, TaskStepState taskStepState)
    {
        Task task = GetTaskById(id);
        task.StoreTaskStepState(taskStepState, stepIndex);
        ChangeTaskState(id, task.state);
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


    private void OnApplicationQuit()
    {
        foreach (Task task in taskMap.Values)
        {
            SaveTask(task);
        }
    }

    private void SaveTask(Task task)
    {
        try
        {
            TaskData taskData = task.GetTaskData();
            // serialize using JsonUtility
            string serializedData = JsonUtility.ToJson(taskData);
            // saving to playerPrefs as a quick solution but could potentially save to file if necessary
            PlayerPrefs.SetString(task.info.id, serializedData);

            //Debug.Log(serializedData);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save task with id " + task.info.id + ": " + e);
        }
    }

    private Task LoadTask(TaskInfoSO taskInfo)
    {
        Task task = null;

        try
        {
            // load task from saved data
            if (PlayerPrefs.HasKey(taskInfo.id) && loadTaskState)
            {
                string serializedData = PlayerPrefs.GetString(taskInfo.id);
                TaskData taskData = JsonUtility.FromJson<TaskData>(serializedData);
                task = new Task(taskInfo, taskData.state, taskData.taskStepIndex, taskData.taskStepStates);
            }
            // otherwise, initialize a new quest
            else
            {
                task = new Task(taskInfo);
            }

        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load task with id " + task.info.id + ": " + e);
        }
        return task;
    }

}
