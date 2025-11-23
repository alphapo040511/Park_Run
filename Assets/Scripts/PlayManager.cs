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

    public Image hp;
    private int bestScore = 0;

    private bool isDie = false;

    private void Start()
    {
        BestScoreUpdate(bestScore);
        randomSeed = Random.Range(0, int.MaxValue);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isDie)
        {
            SceneManager.LoadScene("GameScene");
            isDie = false;
            randomSeed = Random.Range(0, 10000);
        }
    }

    void BestScoreUpdate(int meter)
    {
        bestScoreText.text = $"BEST {meter:N0}M";
    }

    public void ScoreUpdate(int meter)
    {
        if (bestScore < meter)
        {
            bestScore = meter;
            BestScoreUpdate(bestScore);
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
    }
}
