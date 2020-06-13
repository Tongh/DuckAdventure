using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController Controller;
    private FGameState GameState;
    public Text StarTextUI;
    public Text ScoreTextUI;
    public Text HeartsTextUI;
    public int Score { get; set; }
    public int Stars { get; set; }

    private float HeartsReducePeriod = 2.0f;
    private int hearts = 5;
    public int Hearts
    {
        get
        {
            return hearts;
        }
        set
        {
            hearts = Mathf.Clamp(value, 0, 7);
        }
    }

    private void Awake()
    {
        Instance = this;
        StartCoroutine(ReduceHearts());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play()
    {
        GameState = FGameState.Playing;
        
        Controller.Play();
    }

    public void GameOver()
    {
        Controller.Dead();
        StartCoroutine(RestartLevel(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && GameState != FGameState.Playing)
        {
            Play();
        }

        StarTextUI.text = "Stars: " + Stars.ToString();
        ScoreTextUI.text = "Score: " + Score.ToString();
        HeartsTextUI.text = "Hearts: " + Hearts.ToString();
    }

    IEnumerator ReduceHearts()
    {
        while (true)
        {
            yield return new WaitForSeconds(HeartsReducePeriod);
            if (GameState == FGameState.Playing)
                Hearts--;
            if (Hearts <= 0)
            {
                GameOver();
            }
        }
    }

    IEnumerator RestartLevel(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum FGameState
{
    Menu,
    Playing,
    Pause
}
