using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TrenGameManager : MinijuegoController
{
    [SerializeField] private TextMeshProUGUI t_puntos, t_timer, t_distancia;    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject tren;
    [SerializeField] private GameObject sceneLoadManager;
    [SerializeField] private Animator fadeAnimator;
    public static event Action escapaTren, tiempoAlcanzado;
    private float timerTime;
    private int sumaPuntos, n_aleatorio, minutos, segundos, centesimas, targetPuntos;
    [SerializeField] private int puntos;
    private bool controlCorrutina, controlFinal, controlTiempo;

    private void OnEnable()
    {
        ControlCarrera.trenAlcanzado += EmpiezaASumar;
        ControlCarrera.trenSaliendo += DejaDeSumar;
        Play();
    }
    private void OnDisable()
    {
        ControlCarrera.trenAlcanzado -= EmpiezaASumar;
        ControlCarrera.trenSaliendo -= DejaDeSumar;
    }

    private void Awake()
    {
        SetDifficulty();
    }
    void Start()
    {
        puntos = 0;
        controlCorrutina = false;
        controlFinal = false;
        controlTiempo = false;
        StartCoroutine(StartTimer());
    }

    void Update()
    {
        t_puntos.text = puntos.ToString();

        float distancia;
        distancia = Vector2.Distance(player.transform.position, tren.transform.position);
        distancia = Mathf.Round(distancia / 2);

        if (distancia <= 150) t_distancia.text = String.Format("{0:00} m", distancia);

        if (distancia > 150 && !controlFinal)
        {
            //Debug.Log("el tren se ha escapado");
            escapaTren?.Invoke();
            controlFinal = true;
            GameOver();
        }
        if (timerTime >= 90 && !controlTiempo)
        {
            tiempoAlcanzado?.Invoke();
            controlTiempo = true;
        }
    }
    void EmpiezaASumar()
    {
        controlCorrutina = true;
        StartCoroutine(SumaPuntos());
    }
    void DejaDeSumar()
    {
        controlCorrutina = false;
    }
    int GetPuntosASumar()
    {
        n_aleatorio = UnityEngine.Random.Range(0, 2);
        switch (n_aleatorio)
        {
            case 0:
                sumaPuntos = 25;
                break;

            case 1:
                sumaPuntos = 50;
                break;

            case 2:
                sumaPuntos = 75;
                break;
        }
        return sumaPuntos;
    }
    void GameOver()
    {
        if (puntos >= targetPuntos)
        {
            LanzaTransicion();
            Invoke("OnWinGame", 1f);
        }
        else
        {
            LanzaTransicion();
            Invoke("OnLoseGame", 1f);
        }
    }
    void LanzaTransicion()
    {
        sceneLoadManager.SetActive(true);
        fadeAnimator.SetTrigger("StartTransition");
    }
    private void OnWinGame()
    {
        int final = GameManager.instance.GetCurrentGameDifficulty() == Dificultad.Facil ? 1 : (GameManager.instance.GetCurrentGameDifficulty() == Dificultad.Medio ? 2 : 3);
        GameManager.instance.OnWinFinalGame(final);
    }

    private void OnLoseGame()
    {
        if(GameManager.instance.GetCurrentGameDifficulty() == Dificultad.Medio || GameManager.instance.GetCurrentGameDifficulty() == Dificultad.Dificil)
        {
            if(puntos >= targetPuntos / 2)
            {
                GameManager.instance.OnLoseFinalGame();
            }
            else
            {
                GameManager.instance.OnDieFinalGame();
            }
        }
        else
        {
            GameManager.instance.OnDieFinalGame();
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
        targetPuntos = 750;
    }

    public override void DifficultyMedium()
    {
        base.DifficultyMedium();
        targetPuntos = 1000;
    }

    public override void DifficultyHard()
    {
        base.DifficultyHard();
        targetPuntos = 1500;
    }
    IEnumerator SumaPuntos()
    {
        while (controlCorrutina)
        {
            puntos += GetPuntosASumar();
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            timerTime += Time.deltaTime;

            if (timerTime < 0) timerTime = 0;

            minutos = (int)(timerTime / 60f);
            segundos = (int)(timerTime - minutos * 60f);
            centesimas = (int)((timerTime - (int)timerTime) * 100f);

            t_timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);

            if (timerTime == 0)
            {
                //Time.timeScale = 0;
                //Debug.Log("Tiempo parado");
                break;
            }

            yield return null;
        }
    }
}
