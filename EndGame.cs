using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TMP_Text winnerMessage;
    // Start is called before the first frame update
    void Start()
    {
        float timeLeft = PlayerPrefs.GetFloat("TimeRemaining");

        int minutes = (int)timeLeft / 60;
        int seconds = (int)timeLeft % 60;

        winnerMessage.text = "You won with " +  minutes + ":" + seconds.ToString("d2") + " left";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
