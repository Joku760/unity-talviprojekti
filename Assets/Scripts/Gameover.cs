using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    Leaderboard leaderboard;
    int points = 1;
    void Start()
    {
        leaderboard = GetComponent<Leaderboard>();
    }
    public void StartMenu()
    {
        leaderboard.CheckHighScores(points);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
