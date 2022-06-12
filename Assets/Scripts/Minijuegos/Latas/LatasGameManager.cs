using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LatasGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_puntos;
    [SerializeField] private float timerTime;
    [SerializeField] private Texture2D mira;
    [SerializeField] private Vector2 cursorOffset;
    [SerializeField] private CompruebaDisparo compruebaDisparo;
    private int puntos, municion, latasAlcanzadas;

    private void OnEnable()
    {
        Lata.diana += SumaLatasYPuntos;
        Lata.sueloTocado += GameOver;
        CompruebaDisparo.comprobarDisparo += RestaMunicion;
    }
    private void OnDisable()
    {
        Lata.diana -= SumaLatasYPuntos;
        Lata.sueloTocado -= GameOver;
        CompruebaDisparo.comprobarDisparo -= RestaMunicion;
    }
    private void Awake()
    {
        Cursor.SetCursor(mira, cursorOffset, CursorMode.Auto);
        municion = 5;
        latasAlcanzadas = 0;
    }
    private void Update()
    {
        t_puntos.text = puntos.ToString();

        if (timerTime == 0 || municion == 0)
        {
            GameOver();
        }
        if (latasAlcanzadas == 3) RecargaMunicion();
        Debug.Log(municion);
    }
    void SumaLatasYPuntos()
    {
        puntos++;
        latasAlcanzadas++;
    }
    void RestaMunicion()
    {
        municion--;
    }
    void RecargaMunicion()
    {
        municion = 5;
        latasAlcanzadas = 0;
    }
    void GameOver()
    {
        Time.timeScale = 0;
    }
}
