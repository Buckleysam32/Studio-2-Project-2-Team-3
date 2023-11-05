using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        //GameEventsManager.instance.gameEvents.LoadScene += LoadScene;
        //GameEventsManager.instance.gameEvents.QuitGame += QuitGame;
    }

    private void OnDisable()
    {
        //GameEventsManager.instance.gameEvents.LoadScene -= LoadScene;
        //GameEventsManager.instance.gameEvents.QuitGame -= QuitGame;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
