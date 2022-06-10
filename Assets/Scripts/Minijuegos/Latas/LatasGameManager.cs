using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LatasGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_puntos, t_timer;
    [SerializeField] private float timerTime;
    private int minutos, segundos, centesimas, puntos;

    private void OnEnable()
    {
        Lata.diana += SumaPuntos;
        Lata.sueloTocado += GameOver;
    }
    private void OnDisable()
    {
        Lata.diana -= SumaPuntos;
        Lata.sueloTocado -= GameOver;
    }
    private void Start()
    {
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        t_puntos.text = puntos.ToString();

        if (timerTime == 0)
        {
            Time.timeScale = 0;
        }
    }
    void SumaPuntos()
    {
        puntos++;
    }
    void GameOver()
    {
        Time.timeScale = 0;
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
