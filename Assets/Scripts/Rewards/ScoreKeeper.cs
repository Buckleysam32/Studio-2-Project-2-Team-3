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
    private int highscore3;
    private int highscore4;
    private int highscore5;
    private int highscore6;
    private int highscore7;
    private int highscore8;
    private int highscore9;

    public GameObject highscorePanel;
    public TMP_Text highscore0Text;
    public TMP_Text highscore1Text;
    public TMP_Text highscore2Text;
    public TMP_Text highscore3Text;
    public TMP_Text highscore4Text;
    public TMP_Text highscore5Text;
    public TMP_Text highscore6Text;
    public TMP_Text highscore7Text;
    public TMP_Text highscore8Text;
    public TMP_Text highscore9Text;

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
            highscore0Text.text = "1st : " +highscore0;
        }

        if (PlayerPrefs.HasKey("Highscore1"))
        {
            highscore1 = PlayerPrefs.GetInt("Highscore1");
            highscore1Text.text = "2nd : " + highscore1;
        }

        if (PlayerPrefs.HasKey("Highscore2"))
        {
            highscore2 = PlayerPrefs.GetInt("Highscore2");
            highscore2Text.text = "3rd : " + highscore2;
        }

        if (PlayerPrefs.HasKey("Highscore3"))
        {
            highscore3 = PlayerPrefs.GetInt("Highscore3");
            highscore3Text.text = "4th : " + highscore3;
        }

        if (PlayerPrefs.HasKey("Highscore4"))
        {
            highscore4 = PlayerPrefs.GetInt("Highscore4");
            highscore4Text.text = "5th : " + highscore4;
        }

        if (PlayerPrefs.HasKey("Highscore5"))
        {
            highscore5 = PlayerPrefs.GetInt("Highscore5");
            highscore5Text.text = "6th : " + highscore5;
        }

        if (PlayerPrefs.HasKey("Highscore6"))
        {
            highscore6 = PlayerPrefs.GetInt("Highscore6");
            highscore6Text.text = "7th : " + highscore6;
        }

        if (PlayerPrefs.HasKey("Highscore7"))
        {
            highscore7 = PlayerPrefs.GetInt("Highscore7");
            highscore7Text.text = "8th : " + highscore7;
        }

        if (PlayerPrefs.HasKey("Highscore8"))
        {
            highscore8 = PlayerPrefs.GetInt("Highscore8");
            highscore8Text.text = "9th : " + highscore8;
        }

        if (PlayerPrefs.HasKey("Highscore9"))
        {
            highscore9 = PlayerPrefs.GetInt("Highscore9");
            highscore9Text.text = "10th: " + highscore9;
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
        //remove all entries except the top 10
        for (int i = scoreList.Count; i > 10; i--)
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
            // You get the point, go through all until 10th
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore3"))
            {
                PlayerPrefs.SetInt($"Highscore3", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore4"))
            {
                PlayerPrefs.SetInt($"Highscore4", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore5"))
            {
                PlayerPrefs.SetInt($"Highscore5", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore6"))
            {
                PlayerPrefs.SetInt($"Highscore6", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore7"))
            {
                PlayerPrefs.SetInt($"Highscore7", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore8"))
            {
                PlayerPrefs.SetInt($"Highscore8", scoreList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore9"))
            {
                PlayerPrefs.SetInt($"Highscore9", scoreList[i]);
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
