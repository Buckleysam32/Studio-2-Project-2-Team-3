using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onLoadScene += LoadScene;
        GameEventsManager.instance.gameEvents.onQuitGame += QuitGame;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onLoadScene -= LoadScene;
        GameEventsManager.instance.gameEvents.onQuitGame -= QuitGame;
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
