using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LatasGameManager : MinijuegoController
{
    [SerializeField] private TextMeshProUGUI t_puntos;
    [SerializeField] private Texture2D mira;
    [SerializeField] private Vector2 cursorOffset;
    [SerializeField] private Image[] im_municion = new Image[5];
    [SerializeField] private GameObject lata;
    [SerializeField] private GameObject sceneLoadManager;
    [SerializeField] private Animator fadeAnimator;

    private int puntos, targetPuntos, municion, latasAlcanzadas, latasParaRecargar;

    private Vector2 posInstLata;

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
        latasParaRecargar = 10;
        SetDifficulty();
    }
    private void Update()
    {
        t_puntos.text = puntos.ToString();

        if (municion == 0)
        {
            GameOver();
        }
        if (latasAlcanzadas == latasParaRecargar) RecargaMunicion();
    }
    
    void SumaLatasYPuntos()
    {
        puntos++;
        latasAlcanzadas++;
        InstanciaLata();
    }
    void RestaMunicion()
    {
        municion--;
        ActualizaIm_Municion();
    }
    void RecargaMunicion()
    {
        municion = 5;
        latasAlcanzadas = 0;
        ActualizaIm_Municion();
    }
    void ActualizaIm_Municion()
    {
        switch (municion)
        {
            case 0:
                for(int i = 0; i < im_municion.Length; i++)
                {
                    im_municion[i].color = Color.black;
                }
                break;

            case 1:
                for (int i = 0; i < im_municion.Length -1f; i++)
                {
                    im_municion[i].color = Color.black;
                }
                break;

            case 2:
                for (int i = 0; i < im_municion.Length - 2f; i++)
                {
                    im_municion[i].color = Color.black;
                }
                break;

            case 3:
                for (int i = 0; i < im_municion.Length - 3f; i++)
                {
                    im_municion[i].color = Color.black;
                }
                break;

            case 4:
                for (int i = 0; i < im_municion.Length - 4f; i++)
                {
                    im_municion[i].color = Color.black;
                }
                break;

            case 5:
                for (int i = 0; i < im_municion.Length; i++)
                {
                    im_municion[i].color = Color.white;
                }
                break;
        }
    }
    void InstanciaLata()
    {
        if (puntos == 10)
        {
            posInstLata.y = 6.5f;
            posInstLata.x = Random.Range(-8f, 8f);
            Instantiate(lata, posInstLata, Quaternion.identity);
        }
        if (puntos == 30)
        {
            posInstLata.y = 6.5f;
            posInstLata.x = Random.Range(-8f, 8f);
            Instantiate(lata, posInstLata, Quaternion.identity);
        }
        if (puntos == 60)
        {
            posInstLata.y = 6.5f;
            posInstLata.x = Random.Range(-8f, 8f);
            Instantiate(lata, posInstLata, Quaternion.identity);
        }
    }
    void GameOver()
    {
        if (puntos >= targetPuntos)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            LanzaTransicion();
            Invoke("OnWinGame", 1f);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        targetPuntos = 15;
    }

    public override void DifficultyMedium()
    {
        base.DifficultyMedium();
        targetPuntos = 50;
    }

    public override void DifficultyHard()
    {
        base.DifficultyHard();
        targetPuntos = 100;
    }
}
