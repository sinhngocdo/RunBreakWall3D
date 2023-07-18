using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    internal static GameUIHandler instance = null;

    [SerializeField] private TextMeshProUGUI scoreTxt = null;
    [SerializeField] private TextMeshProUGUI hightScoreText = null;
    [SerializeField] private GameObject gamePlayUI = null;
    [SerializeField] private GameObject gameOverUI = null;
    [SerializeField] private GameObject gameMenuUi = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private TextMeshProUGUI musicOnOffText = null;
 
    internal int gameScore = 0;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("Music") == 1)
        {
            musicOnOffText.SetText("Music: On");
            audioSource.Play();
        }
        else
        {
            musicOnOffText.SetText("Music: Off");
            audioSource.Pause();
        }
        hightScoreText.SetText("HighScore: "+ PlayerPrefs.GetInt("HightScore").ToString());
    }

    private void Update()
    {
        scoreTxt.SetText(gameScore.ToString());
    }

    public void PlayBtn()
    {
        GameStart();
    }

    private void GameStart()
    {
        PlayerController.instance.isGameStart = true;
        PlayerController.instance.isGameOver = false;
        PlayerController.instance.anim.SetTrigger("Run");
        gamePlayUI.SetActive(true);
        gameMenuUi.SetActive(false);
        gameOverUI.SetActive(false);
        gameScore = 0;
        Debug.Log("StartGame");
    }

    public void RetryBtn()
    {
        PlayerController.instance.ResetStagePlayer();
        ResetTrackPos.instance.SetupTrack();
        GameStart();
        PlayerController.instance.activeTackManager.ColorChange();
        Debug.Log(PlayerController.instance.isGameOver);
        Debug.Log(PlayerController.instance.isGameStart);
    }

    public void MusicOnOff()
    {
        if(PlayerPrefs.GetInt("Music") == 1)
        {
            musicOnOffText.SetText("Music: Off");
            PlayerPrefs.SetInt("Music", 0);
            audioSource.Pause();
        }
        else
        {
            musicOnOffText.SetText("Music: On");
            PlayerPrefs.SetInt("Music", 1);
            audioSource.Play();
        }
    }

    public void ReturnMenuBtn()
    {
        SceneManager.LoadScene(0);
    }


    internal void GameOver()
    {
        gamePlayUI.SetActive(false);
        gameOverUI.SetActive(true);

        if (gameScore > PlayerPrefs.GetInt("HightScore"))
        {
            PlayerPrefs.SetInt("HightScore", gameScore);
        }
    }



}
