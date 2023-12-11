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

    public List<string> initialList = new List<string>();
    private string highscore0Initial;
    private string highscore1Initial;
    private string highscore2Initial;
    private string highscore3Initial;
    private string highscore4Initial;
    private string highscore5Initial;
    private string highscore6Initial;
    private string highscore7Initial;
    private string highscore8Initial;
    private string highscore9Initial;

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

    public TMP_InputField initialField;

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
        initialField.characterLimit = 3;

        if (PlayerPrefs.HasKey("Highscore0"))
        {
            highscore0 = PlayerPrefs.GetInt("Highscore0");
            highscore0Initial = PlayerPrefs.GetString("Highscore0Inital");
            highscore0Text.text = "1st : " + highscore0 + " " + highscore0Initial;
        }

        if (PlayerPrefs.HasKey("Highscore1"))
        {
            highscore1 = PlayerPrefs.GetInt("Highscore1");
            highscore1Initial = PlayerPrefs.GetString("Highscore1Inital");
            highscore1Text.text = "2nd : " + highscore1 + " " + highscore1Initial;
        }

        if (PlayerPrefs.HasKey("Highscore2"))
        {
            highscore2 = PlayerPrefs.GetInt("Highscore2");
            highscore2Initial = PlayerPrefs.GetString("Highscore2Inital");
            highscore2Text.text = "3rd : " + highscore2 + " " + highscore2Initial;
        }

        if (PlayerPrefs.HasKey("Highscore3"))
        {
            highscore3 = PlayerPrefs.GetInt("Highscore3");
            highscore3Initial = PlayerPrefs.GetString("Highscore3Inital");
            highscore3Text.text = "4th : " + highscore3 + " " + highscore3Initial;
        }

        if (PlayerPrefs.HasKey("Highscore4"))
        {
            highscore4 = PlayerPrefs.GetInt("Highscore4");
            highscore4Initial = PlayerPrefs.GetString("Highscore4Inital");
            highscore4Text.text = "5th : " + highscore4 + " " + highscore4Initial;
        }

        if (PlayerPrefs.HasKey("Highscore5"))
        {
            highscore5 = PlayerPrefs.GetInt("Highscore5");
            highscore5Initial = PlayerPrefs.GetString("Highscore5Inital");
            highscore5Text.text = "6th : " + highscore5 + " " + highscore5Initial;
        }

        if (PlayerPrefs.HasKey("Highscore6"))
        {
            highscore6 = PlayerPrefs.GetInt("Highscore6");
            highscore6Initial = PlayerPrefs.GetString("Highscore6Inital");
            highscore6Text.text = "7th : " + highscore6 + " " + highscore6Initial;
        }

        if (PlayerPrefs.HasKey("Highscore7"))
        {
            highscore7 = PlayerPrefs.GetInt("Highscore7");
            highscore7Initial = PlayerPrefs.GetString("Highscore7Inital");
            highscore7Text.text = "8th : " + highscore7 + " " + highscore7Initial;
        }

        if (PlayerPrefs.HasKey("Highscore8"))
        {
            highscore8 = PlayerPrefs.GetInt("Highscore8");
            highscore8Initial = PlayerPrefs.GetString("Highscore8Inital");
            highscore8Text.text = "9th : " + highscore8 + " " + highscore8Initial;
        }

        if (PlayerPrefs.HasKey("Highscore9"))
        {
            highscore9 = PlayerPrefs.GetInt("Highscore9");
            highscore9Initial = PlayerPrefs.GetString("Highscore9Inital");
            highscore9Text.text = "10th: " + highscore9 + " " + highscore9Initial;
        }
    }

    public void SubmitScore(int score)
    {
        scoreList.Add(score);
        string initalText = initialField.GetComponent<TMP_InputField>().text;
        initialList.Add(initalText);
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
                PlayerPrefs.SetString($"Highscore0Inital", initialList[i]);
            }
            //if not check if it beats the 2nd high score
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore1"))
            {
                PlayerPrefs.SetInt($"Highscore1", scoreList[i]);
                PlayerPrefs.SetString($"Highscore1Inital", initialList[i]);
            }
            // finally check if it beats the 3rd high score
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore2"))
            {
                PlayerPrefs.SetInt($"Highscore2", scoreList[i]);
                PlayerPrefs.SetString($"Highscore2Inital", initialList[i]);
            }
            // You get the point, go through all until 10th
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore3"))
            {
                PlayerPrefs.SetInt($"Highscore3", scoreList[i]);
                PlayerPrefs.SetString($"Highscore3Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore4"))
            {
                PlayerPrefs.SetInt($"Highscore4", scoreList[i]);
                PlayerPrefs.SetString($"Highscore4Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore5"))
            {
                PlayerPrefs.SetInt($"Highscore5", scoreList[i]);
                PlayerPrefs.SetString($"Highscore5Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore6"))
            {
                PlayerPrefs.SetInt($"Highscore6", scoreList[i]);
                PlayerPrefs.SetString($"Highscore6Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore7"))
            {
                PlayerPrefs.SetInt($"Highscore7", scoreList[i]);
                PlayerPrefs.SetString($"Highscore7Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore8"))
            {
                PlayerPrefs.SetInt($"Highscore8", scoreList[i]);
                PlayerPrefs.SetString($"Highscore8Inital", initialList[i]);
            }
            else if (scoreList[i] > PlayerPrefs.GetInt($"Highscore9"))
            {
                PlayerPrefs.SetInt($"Highscore9", scoreList[i]);
                PlayerPrefs.SetString($"Highscore9Inital", initialList[i]);
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
