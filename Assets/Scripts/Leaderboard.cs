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
        DrawScores();
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

    public void CheckHighScores(int value)
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            if(value > highScoreValues[x])
            {
                for(int y = highScores.Length -1; y > x; y--)
                {
                    highScoreValues[y] = highScoreValues[y - 1];
                }
                highScoreValues[x] = value;
                DrawScores();
                SaveScores();
                break;
            }
        }
    }
}
