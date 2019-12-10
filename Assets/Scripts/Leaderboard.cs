using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text[] highScores;
    int[] highScoreValues;

    // Start is called before the first frame update
    void Start()
    {
        highScoreValues = new int[highScores.Length];
        for (int x=0; x < highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
        }
        CheckHighScores();
        DrawScores();
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            PlayerPrefs.SetInt("highScoreValues" + x, highScoreValues[x]);
        }
    }

    void DrawScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            highScores[x].text = highScoreValues[x].ToString();
        }
    }

    public void CheckHighScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            if(PlayerPrefs.GetInt("Score") > highScoreValues[x])
            {
                for(int y = highScores.Length -1; y > x; y--)
                {
                    highScoreValues[y] = highScoreValues[y - 1];
                }
                highScoreValues[x] = PlayerPrefs.GetInt("Score");
                DrawScores();
                SaveScores();
                break;
            }
        }
    }
}
