﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum eScoreType
{
    enemy,
    boss,
}

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public float score;
    public float oldScore;

    public static ScoreManager S;

    // Start is called before the first frame update
    void Start()
    {
        if (oldScore != 0)
        {
            score = oldScore;
        }
        else
        {
            score = 0.0f;
        }
        
        scoreText = GetComponent<Text>();
        DisplayScore();
        S = this;
    }

    static public void EVENT(eScoreType evt)
    {
        try
        {
            S.Event(evt);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() called while S=null\n" + nre);
        }
    }

    void Event(eScoreType evt)
    {
        switch (evt)
        {
            case eScoreType.enemy:
                score += 100;
                DisplayScore();
                break;
            case eScoreType.boss:
                score += 1000000;
                DisplayScore();
                break;
        }

    }

    private void DisplayScore()
    {
        scoreText.text = "SCORE: " + score.ToString("N0");
        NextSceneCheck();
    }

    private void NextSceneCheck()
    {
        if (score == 10000)
        {
            oldScore = score;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
