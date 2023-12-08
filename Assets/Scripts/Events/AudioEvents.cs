using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents
{
    public event Action<string> onPlayOneShot;
    /// <summary>
    /// Plays a oneshot of the sound effect
    /// </summary>
    /// <param name="name"></param>
    public void PlayOneShot(string name)
    {
        if (onPlayOneShot != null)
        {
            onPlayOneShot(name);
        }
    }

    public event Action<string> onPlay;
    /// <summary>
    /// plays audioclip on loop
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        if (onPlay != null)
        {
            onPlay(name);
        }
    }

    public event Action onStop;

    public void Stop()
    {
        if (onStop != null)
        {
            onStop();
        }
    }
}
