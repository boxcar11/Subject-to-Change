using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public GameObject[] CardUI;
    public GameObject[] chemicalUI;

    public TMP_Text clock;

    public GameObject messagePanel;
    public TMP_Text messageText;

    public Image fadePanel;

    public GameObject WarpCore;

    public AudioMixer mixer;

    public Canvas menuCanvas;
    public Slider volumeSlider;

    public int startTime;
    public int levelToLoad;

    private List<Cards> cards;
    private List<Chemicals> chemicals;

    private float timeLeft;

    private bool menuStatus;

    private PlayerController player;

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        cards = new List<Cards>();
        chemicals = new List<Chemicals>();

        timeLeft = startTime;

        float value;
        bool result = mixer.GetFloat("MasterVolume", out value);
        if(result)
        {
            volumeSlider.value = Mathf.Pow(10, value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        int minutes = (int)timeLeft / 60;
        int seconds = (int)timeLeft % 60;

        clock.text = minutes + ":" + seconds.ToString("d2");

        if(timeLeft <= 0)
        {
            ReloadLevel();
        }
    }

    public void AddKeyCard(Cards card)
    {
        cards.Add(card);

        CardUI[(int)card].SetActive(true);
    }

    public void AddChemical(Chemicals chemical)
    {
        chemicals.Add(chemical);
        chemicalUI[(int)chemical].SetActive(true);
    }

    public bool CheckForKeyCard(Cards card)
    {
        if(cards.Contains(card))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForChemical(Chemicals chemical)
    {
        if(chemicals.Contains(chemical))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveChemicals(Chemicals chemical)
    {
        chemicals.Remove(chemical);
        chemicalUI[(int)chemical].SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void DisplayMessage(string message)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }

    public void ShutDownCore()
    {
        WarpCore.SetActive(false);
        PlayerPrefs.SetFloat("TimeRemaining", timeLeft);
        PlayerPrefs.Save();
        SceneManager.LoadScene(3);
    }

    #region Menu
    public void ToggleMenu()
    {
        menuStatus = !menuStatus;
        menuCanvas.enabled = menuStatus;

        if(menuStatus)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        ToggleMenu();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        ReloadLevel();
    }

    public void Volume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    public IEnumerator Fade(int fadeDirection)
    {
        if (fadeDirection == 0)
        {
            if (player != null)
            {
                player.SetMoveAllowed(false);
            }
            for (float alpha = 0f; alpha < 1.0f; alpha += Time.deltaTime)
            {
                fadePanel.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        else if(fadeDirection == 1)
        {
            for (float alpha = 1.0f; alpha > 0f; alpha -= Time.deltaTime)
            {
                fadePanel.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            if (player != null)
            {
                player.SetMoveAllowed(true);
            }
        }
    }

    public IEnumerator Fade(int fadeDirection, Stairs stair)
    {
        if (fadeDirection == 0)
        {
            if (player != null)
            {
                player.SetMoveAllowed(false);
            }
            for (float alpha = 0f; alpha < 1.0f; alpha += Time.deltaTime)
            {
                fadePanel.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            
        }
        else if (fadeDirection == 1)
        {
            for (float alpha = 1.0f; alpha > 0f; alpha -= Time.deltaTime)
            {
                fadePanel.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            if (player != null)
            {
                player.SetMoveAllowed(true);
            }
        }

        stair.MovePosition();
    }

    public IEnumerator WaitToFade(int fadeDirection)
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(GameManager.Instance.Fade(fadeDirection));
    }
    public IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(1);
        ReloadLevel();
    }
}

public enum Cards
{
    Red,
    Green,
    Blue,
    Yellow
}

public enum Chemicals
{
    Red,
    Green,
    Blue
}
