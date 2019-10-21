using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public static ScoreManager S;

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
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
        scoreText.text = "SCORE: " + score;
    }

}
