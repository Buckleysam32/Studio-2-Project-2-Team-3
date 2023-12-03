using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents 
{
    public event Action<string> onSubmitPressed;

    /// <summary>
    /// called when the player presses the submit button
    /// </summary>
    public void SubmitPressed(string id)
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed(id);
        }
    }

    public event Action onPausePressed;

    public void PausePressed()
    {
        if (onPausePressed != null)
        {
            onPausePressed();
        }
    }

}
