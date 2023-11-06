using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject requirementsNotMetIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject inProgressIcon;
    [SerializeField] private GameObject canFinishIcon;


    public void SetState(TaskState newState, bool startPoint, bool endPoint)
    {
        //set all to inactive
        requirementsNotMetIcon.SetActive(false);
        canStartIcon.SetActive(false);
        inProgressIcon.SetActive(false);
        canFinishIcon.SetActive(false);


        // set the appropriate one to active based on the new state
        switch (newState)
        {
            case TaskState.RequirementsNotMet:
                if(startPoint) { requirementsNotMetIcon.SetActive(true); }
                break;
            case TaskState.CanStart:
                if (startPoint)
                {
                    canStartIcon.SetActive(true);
                    GameEventsManager.instance.uiEvents.onActivateIndicator(this.gameObject);
                }
                break;
            case TaskState.InProgress:
                if (endPoint)
                {
                    inProgressIcon.SetActive(true);
                }
                break;
            case TaskState.CanFinish:
                if (endPoint) { canFinishIcon.SetActive(true); }
                break;
            case TaskState.Finished:
                break;
            default:
                Debug.LogError("Quest State not recognised by switch state for task icon: " + newState);
                break;
        }
    }
}
