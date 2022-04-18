using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void ReplayGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
    public void Level2()
    {
        SceneManager.LoadScene("Game 2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Game 3");
    }
}
