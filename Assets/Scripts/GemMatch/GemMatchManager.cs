using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemMatchManager : MonoBehaviour
{
    public static GemMatchManager Instance;

    public TMP_Text ScoreText;
    public TMP_Text TimerText;
    public float startTime = 20;
    public float maxScore = 20;
    public GameObject winScreen;
    public GameObject loseScreen;


    public bool gameOver = false;
    private int score = 0;
    float timer;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private int envioronmentVersion;

    [System.Serializable]
    public class Envioronment
    {
        public Sprite dangerEnv;
        public Sprite safeEnv;
    }

    public Sprite[] gemSprites;

    public Animator characterAnimtator;
    public SpriteRenderer envioronmentRenderer;
    public Envioronment[] envioronments;

    private void Start()
    {

        envioronmentVersion = Random.Range(0, envioronments.Length);

        envioronmentRenderer.sprite = envioronments[envioronmentVersion].dangerEnv;

        characterAnimtator.SetInteger("Env", envioronmentVersion);

        ScoreText.text = "0 / " + maxScore;
        timer = startTime;
        TimerText.text = FormatTime(timer);
        //startGame
    }

    private string FormatTime(float time)
    {
        return (((int)(time * 100)) / 100f).ToString();
    }

    private void Update()
    {
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            TimerText.text = FormatTime(timer);

            if (timer <= 0)
                OnGameLose();
        }

    }

    public void OnGameLose()
    {
        characterAnimtator.SetTrigger("Defeat");
        gameOver = true;

        StartCoroutine(ShowScreen(false));
    }


    public void OnGameWin()
    {
        characterAnimtator.SetTrigger("Victory");
        envioronmentRenderer.sprite = envioronments[envioronmentVersion].safeEnv;
        gameOver = true;

        StartCoroutine(ShowScreen(true));
    }

    private IEnumerator ShowScreen(bool win)
    {
        yield return new WaitForSeconds(3f);

        if (win)
            winScreen.SetActive(true);
        else
            loseScreen.SetActive(true);
    }

    public void IncreaseScore(int amount = 1)
    {
        score += amount;
        ScoreText.text = score + " / " + maxScore;

        if (score >= maxScore)
        {
            OnGameWin();
        }
    }

    public void returnToMainMenu()
    {
        SceneLoader.Instance?.Load("MainMenu");
    }

}
