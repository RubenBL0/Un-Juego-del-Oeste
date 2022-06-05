using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_puntos;
    [SerializeField] private TextMeshProUGUI t_timer;
    [SerializeField] private GameObject vaquilla;
    [SerializeField] private Collider2D suelo;
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;
    private int n_vacas, puntos, puntosAnteriores, minutos, segundos, centesimas;    

    private void OnEnable()
    {
        Vaquillas.establoEntrando += SumaVacas;
        Vaquillas.establoSaliendo += RestaVacas;
    }
    private void OnDisable()
    {
        Vaquillas.establoEntrando -= SumaVacas;
        Vaquillas.establoSaliendo -= RestaVacas;
    }
    void Start()
    {
        puntos = 0;
        puntosAnteriores = 0;
        StartCoroutine(SumaPuntos());
        StartCoroutine(StartTimer());
    }
    void Update()
    {
        t_puntos.text = puntos.ToString();

        if (puntos > puntosAnteriores + 20)
        {
            Instantiate(vaquilla, GetPosSpawn(), Quaternion.identity);
            ActualizaPuntosAnteriores();
        }
    }
    Vector2 GetPosSpawn()
    {
        Vector2 vaquillaPos = Vector2.zero;
        Bounds bounds = suelo.bounds;
        vaquillaPos.x = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
        vaquillaPos.y = Random.Range(bounds.min.y + 1, bounds.max.y - 1);
        return vaquillaPos;
    }
    void ActualizaPuntosAnteriores()
    {
        puntosAnteriores = puntos;
    }
    void SumaVacas()
    {
        n_vacas++;
    }
    void RestaVacas()
    {
        n_vacas--;
    }
    IEnumerator SumaPuntos()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            puntos += 1 * n_vacas;
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

            t_timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);

            if (timerTime == 0) 
            {
                Time.timeScale = 0;
                Debug.Log("Tiempo parado");
                break;
            }

            yield return null;
        }
    }
}