using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isFinished;
    public GameObject finishPanel;


    public static bool isGameStarted;
    public GameObject StartingText;

    public static int numberOfCoins;
    public Text coinsText;

    private ScoreManager theScoreManager;
    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        numberOfCoins = 0;
        gameOver = false;
        isFinished = false;
        Time.timeScale = 1;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            Time.timeScale = 0;
            finishPanel.SetActive(true);
        }
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = ""+numberOfCoins;
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(StartingText);
            theScoreManager.scoreIncreasing = true;
        }
    }
}
