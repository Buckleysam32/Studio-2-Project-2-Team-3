using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] TMP_Text textField;
    [SerializeField] string textPrefix; //Characters that appear in front of the evented string
    private bool doingFeedback = false;
    private float timePass = 0;
    [SerializeField] float showFeedback; //How long to show the feedback for

    void Update()
    {
        if (doingFeedback)
        {
            timePass += Time.deltaTime;
            if(timePass > showFeedback)
            {
                FeedbackEnd();
            }
        }
    }
    /// <summary>
    /// Intended for event, use only the changes, not the sum total.
    /// Sets the text using the feedback, then enables the textField object.
    /// Also sets color of text for if positive or negative, default option being positive.
    /// </summary>
    /// <param name="feedback"></param>
    public void FeedbackStart(string feedback, bool isPositive = true)
    {
        GameObject textObject = textField.GetComponentInParent<GameObject>();
        textObject.SetActive(true);
        if (isPositive)
        {
            textField.color = new Color(0, 212, 38);
        }
        else
        {
            textField.color = new Color(212, 0, 0);
        }
        textField.text = textPrefix + feedback;
        doingFeedback = true;
    }
    /// <summary>
    /// Fades out the textField, then disables the object.
    /// </summary>
    void FeedbackEnd()
    {
        textField.alpha -= Time.deltaTime;
        if (textField.alpha <= 0)
        {
            GameObject textObject = textField.GetComponentInParent<GameObject>();
            textObject.SetActive(false);
            doingFeedback = false;
            timePass = 0;
        }
    }

}
