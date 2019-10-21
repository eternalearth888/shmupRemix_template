using System.Collections;
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
            oldScore = 0;
        }

        scoreText = GetComponent<Text>();
        S = this;
    }

    void Event(eScoreType evt)
    {
        switch (evt)
        {
            case eScoreType.enemy:
                score += 100;
                DisplayScore();
                NextSceneCheck() ;
                break;
            case eScoreType.boss:
                score += 1000000;
                DisplayScore();
                NextSceneCheck();
                break;
            default:
                score += 0;
                DisplayScore();
                NextSceneCheck();
                break;
        }

    }

    private void DisplayScore()
    {
        scoreText.text = "SCORE: " + score.ToString("N0");
    }

    private void NextSceneCheck()
    {
        // next level every 1000 points
        if (score > oldScore+1000 )
        {
            oldScore = score;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
