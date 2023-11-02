using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    // All the events for our game should be stored here in event specific classes

    public TaskEvents taskEvents;

    public RewardEvents rewardEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Evenets Manager in the scene.");
        }

        instance = this;

        //initialise all the events
        taskEvents = new TaskEvents();
        rewardEvents = new RewardEvents();
    }

}
