using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused = false;

    public SceneLoader sceneLoader; // this can get removed when game events is set up

    private void OnEnable()
    {
        //GameEventsManager.instance.inputEvents.onPausePressed += Pause;
    }

    private void OnDisable()
    {
        //GameEventsManager.instance.inputEvents.onPausePressed -= Pause;
    }

    // Update is called once per frame
    void Update()
    {
        // need to change this to use the new input system
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Q) && paused)
        {
            //GameEventsManager.instance.gameEvents.QuitGame();
            sceneLoader.QuitGame();
            Debug.Log("Quit Game");
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
