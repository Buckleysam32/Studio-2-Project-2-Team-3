using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePannel;
    public bool paused = false;

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
    }

    public void Pause()
    {
        paused = !paused;

        pausePannel.SetActive(paused);

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
