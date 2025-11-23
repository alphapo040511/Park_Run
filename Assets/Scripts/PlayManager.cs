using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public static int randomSeed = 0;

    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isPlaying = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public TextMeshProUGUI gameOverScore;

    public Image hp;

    public GameObject startUI;
    public GameObject overUI;

    private int bestScore = 0;
    private int lastScore = 0;

    private bool isDie = false;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
        BestScoreUpdate(bestScore);
        randomSeed = Random.Range(0, int.MaxValue);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isDie)
        {
            //Restart();
        }
    }

    public void GameStart()
    {
        isPlaying = true;
        startUI.SetActive(false);
    }

    public void Restart()
    {
        if (isDie == false) return;
        
        randomSeed = Random.Range(0, 10000);
        SceneManager.LoadScene("GameScene");
        isDie = false;

        startUI.SetActive(true);
        overUI.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void BestScoreUpdate(int meter)
    {
        bestScoreText.text = $"BEST {meter:N0}M";
    }

    public void ScoreUpdate(int meter)
    {
        lastScore = meter;
        if (bestScore < meter)
        {
            bestScore = meter;
            BestScoreUpdate(bestScore);
            PlayerPrefs.SetInt("bestScore", bestScore);
        }

        scoreText.text = $"{meter:N0}M";
    }

    public void HpUpdate(float amount)
    {
        amount = Mathf.Clamp01(amount);
        hp.fillAmount = amount;
    }

    public void Die()
    {
        isDie = true;
        isPlaying = false;
        gameOverScore.text = $"{lastScore:N0}M";
        overUI.SetActive(true);
    }
}
