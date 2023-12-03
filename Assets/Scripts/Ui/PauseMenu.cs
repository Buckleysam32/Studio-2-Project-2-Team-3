using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused = false;
    [SerializeField] RewardManager rewardsManagerScript;
    [SerializeField] GameManager gameManagerScript;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onPausePressed += Pause;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onPausePressed -= Pause;
    }

    // Update is called once per frame
    void Update()
    {
        // need to change this to use the new input system
        if (Input.GetKeyDown(KeyCode.Escape) && gameManagerScript.isGameOverScreen == false)
        {
            GameEventsManager.instance.inputEvents.PausePressed();
        }

        if (Input.GetKeyDown(KeyCode.Q) && paused)
        {
            GameEventsManager.instance.gameEvents.QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            GameEventsManager.instance.inputEvents.PausePressed();
            rewardsManagerScript.currentSeconds = 1;
        }
        
    }

    public void Pause()
    {
        paused = !paused;

        pausePanel.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
