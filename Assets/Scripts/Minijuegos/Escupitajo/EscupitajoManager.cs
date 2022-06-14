using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EscupitajoManager : MinijuegoController
{
    public static EscupitajoManager instance = null;

    public bool playing = true;
    Coroutine timeCoroutine = null;

    [SerializeField] int score = 0;
    int scoreNeeded;

    [SerializeField] float gameTime = 30f;
    [SerializeField] TextMeshProUGUI txtTime;
    [SerializeField] TextMeshProUGUI txtScore;

    [SerializeField] Spittle escupitajo;
    [SerializeField] PowerBar barra;
    [SerializeField] GameObject caldero;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing && timeCoroutine == null)
        {
            timeCoroutine = StartCoroutine(TimeCoroutine());
        }
    }

    IEnumerator TimeCoroutine()
    {
        while(gameTime > 0f)
        {
            gameTime -= Time.deltaTime;
            WriteTime();
            yield return null;
        }
        EndGame();
        yield return null;
    }

    void WriteTime()
    {
        if (gameTime < 0f)
        {
            gameTime = 0f;
        }
        txtTime.text = TimeSpan.FromSeconds(gameTime).ToString("ss\\.f");
    }

    public void Restart()
    {
        barra.HideBar();
        escupitajo.Restart();
        MoveCauldron();
    }

    public void AddScore(int amount)
    {
        score += amount;
        gameTime += 1f;
        txtScore.text = score.ToString();
    }

    void MoveCauldron()
    {
        float posx = UnityEngine.Random.Range(0f, 10f) - 7.5f; //Número entre -7.5 y 2.5
        caldero.transform.position = new Vector3(posx, -1.5f, 0f);
    }

    void EndGame()
    {
        playing = false;
        barra.gameObject.SetActive(false);
        escupitajo.gameObject.SetActive(false);
        if(score >= scoreNeeded)
        {
            GameManager.instance.OnWinGame();
        }
        else
        {
            GameManager.instance.OnLoseGame();
        }
    }


    //Métodos de MinijuegoController
    public void SetDifficulty()
    {
        switch (GameManager.instance.GetCurrentGameDifficulty())
        {
            case Dificultad.Facil:
                DifficultyEasy();
                break;
            case Dificultad.Medio:
                DifficultyMedium();
                break;
            case Dificultad.Dificil:
                DifficultyHard();
                break;
        }
    }
    public override void DifficultyEasy()
    {
        base.DifficultyEasy();
        scoreNeeded = 5;
    }

    public override void DifficultyMedium()
    {
        base.DifficultyMedium();
        scoreNeeded = 10;
    }

    public override void DifficultyHard()
    {
        base.DifficultyHard();
        scoreNeeded = 15;
    }
}
