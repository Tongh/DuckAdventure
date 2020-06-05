using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController Controller;
    public Text StarTextUI;
    public Text ScoreTextUI;
    public int Score { get; set; }
    public int Stars { get; set; }

    private void Awake()
    {
        Instance = this;

        Score = 0;
        Stars = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Controller.Play();
        }

        StarTextUI.text = "Stars: " + Stars.ToString();
        ScoreTextUI.text = "Score: " + Score.ToString();
    }
}
