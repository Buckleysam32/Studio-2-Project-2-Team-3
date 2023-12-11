using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TaskPoint : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoSOForPoint;

    private string taskId;
    private TaskState currentTaskState;
    //private TaskIcon taskIcon;


    [Header("Config")]
    [SerializeField] private bool startPoint = false;
    [SerializeField] private bool endPoint = false;

    public TargetIndicator indicater;

    private void Awake()
    {
        taskId = taskInfoSOForPoint.id;
        //taskIcon = GetComponentInChildren<TaskIcon>();
        indicater = GameObject.FindGameObjectWithTag("Indicator").GetComponent<TargetIndicator>();
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
        //just being used to check if it worked, this should go into an actual input system
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        //{
        //    GameEventsManager.instance.inputEvents.SubmitPressed();
        //}
    }


    private void SubmitPressed(string id)
    {
        // start or finish a task
        if (currentTaskState.Equals(TaskState.CanStart) && startPoint)
        {
            GameEventsManager.instance.taskEvents.StartTask(id);
        }
        else if (currentTaskState.Equals(TaskState.CanFinish) && endPoint)
        {
            GameEventsManager.instance.taskEvents.CompleteTask(id);
        }
    }

    private void TaskStateChange(Task task)
    {
        // only update the task state if this point has the corresponding task
        if (task.info.id.Equals(taskId))
        {
            currentTaskState = task.state;
            //taskIcon.SetState(currentTaskState, startPoint, endPoint);
            if (currentTaskState.Equals(TaskState.CanStart))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                indicater.Target = transform.GetChild(0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentTaskState.Equals(TaskState.CanStart))
        {
            GameEventsManager.instance.inputEvents.SubmitPressed(taskId);
            // turn off the visuals
            GameObject childGO = transform.GetChild(0).gameObject;
            childGO.SetActive(false);
        }
    }
}
