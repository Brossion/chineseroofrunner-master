using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsForLv2 : MonoBehaviour
{

    public void ReplayGame()
    {
        SceneManager.LoadScene("Game 2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Level3()
    {
        SceneManager.LoadScene("Game 3");
    }
    public void Level1()
    {
        SceneManager.LoadScene("Game");
    }
}

