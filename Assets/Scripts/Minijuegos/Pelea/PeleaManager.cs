using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PeleaManager : MinijuegoController
{
    [SerializeField] private GameObject player, enemigo, botella;
    [SerializeField] private TextMeshProUGUI t_temporizador;
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;
    [SerializeField] private Transform[] spawnZones = new Transform[3];
    [SerializeField] private int n_enemigos;
    [SerializeField] private Collider2D barraBar;
    [SerializeField] private GameObject sceneLoadManager;
    [SerializeField] private Animator fadeAnimator;

    public static event Action instanciaEnemigos;

    private int minutos, segundos, centesimas, rnum, tamanoOleada;
    private float tiempoNecesario;

    #region Suscribe Eventos
    private void OnEnable()
    {
        Enemigo.sumaEnemigos += SumaEnemigos;
        Enemigo.restaEnemigos += RestaEnemigos;
        Player.muerte += GameOver;
    }
    private void OnDisable()
    {
        Enemigo.sumaEnemigos -= SumaEnemigos;
        Enemigo.restaEnemigos -= RestaEnemigos;
        Player.muerte -= GameOver;
    }
    #endregion

    private void Awake()
    {
        SetDifficulty();
    }
    void Start()
    {
        n_enemigos = 0;
        tamanoOleada = 5;
        StartCoroutine(StartTimer());
        InvokeRepeating("IniciaOleadas", 0f, 40f);
    }
    private void Update()
    {
        tiempoNecesario = minutos;
        if (timerTime == 0 && n_enemigos == 0)
        {
            GameOver();
        }
    }
    void IniciaOleadas()
    {
        if(timerTime != 0)
        {
            Vector3 posBotella = Vector3.zero;
            Bounds bounds = barraBar.bounds;
            posBotella.x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            posBotella.y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
            posBotella.z = 0;
            Instantiate(botella, posBotella, Quaternion.identity);
            StartCoroutine(InstanciaEnemigos(tamanoOleada));
            tamanoOleada += 2;
        }        
    }
    Vector2 GetSpawnZone()
    {
        rnum = UnityEngine.Random.Range(0, 3);
        return spawnZones[rnum].position;
    }
    void SumaEnemigos()
    {
        n_enemigos++;
    }
    void RestaEnemigos()
    {
        n_enemigos--;
    }
    void GameOver()
    {
        if (tiempoNecesario >= timerTime)
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
        GameManager.instance.OnWinGame();
    }

    private void OnLoseGame()
    {
        GameManager.instance.OnLoseGame();
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
        tiempoNecesario = 150f;
    }

    public override void DifficultyMedium()
    {
        base.DifficultyMedium();
        tiempoNecesario = 90f;
    }

    public override void DifficultyHard()
    {
        base.DifficultyHard();
        tiempoNecesario = 30f;
    }
    IEnumerator InstanciaEnemigos(int tamanoOleada)
    {
        for (int i = 0; i < tamanoOleada; i++)
        {
            Instantiate(enemigo, GetSpawnZone(), Quaternion.identity);
            instanciaEnemigos?.Invoke();
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            timerTime -= Time.deltaTime;

            if (timerTime < 0) timerTime = 0;

            minutos = (int)(timerTime / 60f);
            segundos = (int)(timerTime - minutos * 60f);
            centesimas = (int)((timerTime - (int)timerTime) * 100f);

            t_temporizador.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);

            if (timerTime == 0)
            {
                break;
            }

            yield return null;
        }
    }
}
