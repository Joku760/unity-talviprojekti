using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    public void StartMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
