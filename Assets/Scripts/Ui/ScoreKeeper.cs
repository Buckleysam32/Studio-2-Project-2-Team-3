using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public List<int> scoreList = new List<int>();
    private int highscore0;
    private int highscore1;
    private int highscore2;

    public GameObject highscorePanel;
    public TMP_Text highscore0Text;
    public TMP_Text highscore1Text;
    public TMP_Text highscore2Text;

    public void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onSetHighScore += SubmitScore;
    }

    public void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onSetHighScore -= SubmitScore;
    }

    public void Start()
    {
        // initialize the highscores

        if (PlayerPrefs.HasKey("Highscore0"))
        {
            highscore0 = PlayerPrefs.GetInt("Highscore0");
            highscore0Text.text = "1st : $" +highscore0;
        }

        if (PlayerPrefs.HasKey("Highscore1"))
        {
            highscore1 = PlayerPrefs.GetInt("Highscore1");
            highscore1Text.text = "2nd : $" + highscore1;
        }

        if (PlayerPrefs.HasKey("Highscore2"))
        {
            highscore2 = PlayerPrefs.GetInt("Highscore2");
            highscore2Text.text = "3rd : $" + highscore2;
        }
    }

    public void SubmitScore(int score)
    {
        scoreList.Add(score);
        SortTopScore();
        SetHighScores();
    }

    public void SortTopScore()
    {
        // sort the list from lowest to highest value
        scoreList.Sort();
        // reverse the list since we want the highest values
        scoreList.Reverse();
        //remove all entries except the top 3
        for (int i = scoreList.Count; i > 3; i--)
        {
            scoreList.Remove(i);
        }
    }

    public void SetHighScores()
    {
        // go through the list of highscores
        for (int i = 0; i < scoreList.Count; i++)
        {
            //check if it beats the current top high score
            if (scoreList[i] > PlayerPrefs.GetInt($"Highscore0"))
            {
                PlayerPrefs.SetInt($"Highscore0", scoreList[i]);
            }
            //if not check if it beats the 2nd high score
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore1"))
            {
                PlayerPrefs.SetInt($"Highscore1", scoreList[i]);
            }
            // finally check if it beats the 3rd high score
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore2"))
            {
                PlayerPrefs.SetInt($"Highscore2", scoreList[i]);
            }
        }
    }

    private void Update()
    {
        
        if (Input.anyKey && highscorePanel!= null)
        {
            highscorePanel.SetActive(false);
        }
    }
}
