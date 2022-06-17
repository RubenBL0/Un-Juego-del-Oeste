using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PeleaManager : MonoBehaviour
{
    [SerializeField] private GameObject player, enemigo;
    [SerializeField] private TextMeshProUGUI t_temporizador;
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;
    [SerializeField] private Transform[] spawnZones = new Transform[3];
    [SerializeField] private int n_enemigos;

    public static event Action instanciaEnemigos;

    private int minutos, segundos, centesimas, rnum, tamanoOleada;

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

    void Start()
    {
        n_enemigos = 0;
        tamanoOleada = 5;
        StartCoroutine(StartTimer());
        InvokeRepeating("IniciaOleadas", 0f, 40f);
    }
    private void Update()
    {
        if (timerTime == 0 && n_enemigos == 0)
        {
            GameOver();
        }
    }
    void IniciaOleadas()
    {
        if(timerTime != 0)
        {
            StartCoroutine(InstanciaEnemigos(tamanoOleada));
            tamanoOleada += 2;
        }        
    }
    void GameOver()
    {
        Time.timeScale = 0f;
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
