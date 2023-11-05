using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public event Action onGameStart;

    public void GameStart()
    {
        if (onGameStart != null)
        {
            onGameStart();
        }
    }

    public event Action<float> onTimerStart;

    public void TimerStart(float seconds)
    {
        if (onTimerStart != null)
        {
            onTimerStart(seconds);
        }
    }

    public event Action onTimerEnd;

    public void TimerEnd()
    {
        if (onTimerEnd != null)
        {
            onTimerEnd();
        }
    }

    public event Action onContinueGame;

    public void ContinueGame()
    {
        if (onContinueGame != null)
        {
            onContinueGame();
        }
    }

    public event Action onGameOver;

    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public event Action<int> onLoadScene;

    public void LoadScene(int buildIndex)
    {
        if (onLoadScene != null)
        {
            onLoadScene(buildIndex);
        }
    }

    public event Action onQuitGame;

    public void QuitGame()
    {
        if (onQuitGame != null)
        {
            onQuitGame();
        }
    }

    public event Action<int> onSetHighScore;

    public void SetHighScore(int score)
    {
        if (onSetHighScore != null)
        {
            onSetHighScore(score);
        }
    }
}
