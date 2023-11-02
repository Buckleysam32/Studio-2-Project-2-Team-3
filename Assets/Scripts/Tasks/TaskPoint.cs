using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TaskPoint : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoSOForPoint;

    private bool playerIsNear = false;
    private string taskId;
    private TaskState currentTaskState;
    private TaskIcon taskIcon;


    [Header("Config")]
    [SerializeField] private bool startPoint = false;
    [SerializeField] private bool endPoint = false;

    private void Awake()
    {
        taskId = taskInfoSOForPoint.id;
        taskIcon = GetComponentInChildren<TaskIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange += TaskStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange -= TaskStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    private void Update()
    {
        //just being used to check if its worked, this should go into an actual input system
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }
    }

    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }

        // start, progress or finish a task
        if (currentTaskState.Equals(TaskState.CanStart) && startPoint)
        {
            GameEventsManager.instance.taskEvents.StartTask(taskId);
        }
        else if (currentTaskState.Equals(TaskState.InProgress) && endPoint)
        {
            GameEventsManager.instance.taskEvents.PackageDelivery(taskId);
        }
        else if (currentTaskState.Equals(TaskState.CanFinish) && endPoint)
        {
            GameEventsManager.instance.taskEvents.CompleteTask(taskId);
        }
        

    }

    private void TaskStateChange(Task task)
    {
        // only update the task state if this point has the corresponding task
        if (task.info.id.Equals(taskId))
        {
            currentTaskState = task.state;
            taskIcon.SetState(currentTaskState, startPoint, endPoint);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
