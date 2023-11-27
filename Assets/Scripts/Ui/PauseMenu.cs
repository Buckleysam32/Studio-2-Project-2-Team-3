using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused = false;
    public GameObject miniMap;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameEventsManager.instance.inputEvents.PausePressed();
        }

        if (Input.GetKeyDown(KeyCode.Q) && paused)
        {
            GameEventsManager.instance.gameEvents.QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            Time.timeScale = 1.0f; 
            GameEventsManager.instance.gameEvents.LoadScene(0);
        }
        
    }

    public void Pause()
    {
        paused = !paused;

        pausePanel.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
            miniMap.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            miniMap.SetActive(true);
        }
    }
}
