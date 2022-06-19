using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinijuegoTrigger : MonoBehaviour 
{
    [SerializeField] Minijuego minigame;

    [SerializeField] Canvas enterCanvas;
    [SerializeField] Canvas infoCanvas;

    public BotonDificultad botonFacil;
    public BotonDificultad botonMedio;
    public BotonDificultad botonDificil;

    public GameObject estrellaVacia;
    public GameObject estrellaBronce;
    public GameObject estrellaPlata;
    public GameObject estrellaOro;

    public static GameObject triggersParent = null;

    private void Awake()
    {
        if (triggersParent == null)
        {
            DontDestroyOnLoad(this.transform.parent.gameObject);
            triggersParent = this.transform.parent.gameObject;
        }
        else if (triggersParent != this.transform.parent.gameObject)
        {
            print("destruir");
            Destroy(this.transform.parent.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        infoCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCanvas()
    {
        if (!minigame.requiresStars)
        {
            enterCanvas.gameObject.SetActive(true);
        }
        else if (GameManager.instance.minigamesStatus != MinigameScore.None)
        {
            enterCanvas.gameObject.SetActive(true);
        }
    }

    public void HideCanvas()
    {
        enterCanvas.gameObject.SetActive(false);
    }

    public void InfoCanvas()
    {
        if (!minigame.requiresStars)
        {
            Time.timeScale = 0f;
            GameManager.instance.isMinigameCanvasShown = true;
            infoCanvas.worldCamera = Camera.main;
            infoCanvas.gameObject.SetActive(true);
            ActivateButtons();
        }
        else if(GameManager.instance.minigamesStatus != MinigameScore.None)
        {
            Time.timeScale = 0f;
            GameManager.instance.isMinigameCanvasShown = true;
            infoCanvas.worldCamera = Camera.main;
            infoCanvas.gameObject.SetActive(true);
            ActivateButtons();
        }
    }
    
    public void HideInfoCanvas()
    {
        Time.timeScale = 1f;
        GameManager.instance.isMinigameCanvasShown = false;
        infoCanvas.gameObject.SetActive(false);
    }

 

    public void ActivateButtons()
    {
        if (minigame.requiresStars)
        {
            switch (GameManager.instance.minigamesStatus)
            {
                case MinigameScore.None:
                    botonFacil.enabled = false;
                    botonMedio.enabled = false;
                    botonDificil.enabled = false;

                    estrellaVacia.SetActive(true);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Bronze:
                    botonFacil.enabled = true;
                    botonMedio.enabled = false;
                    botonDificil.enabled = false;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(true);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Silver:
                    botonFacil.enabled = true;
                    botonMedio.enabled = true;
                    botonDificil.enabled = false;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(true);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Gold:
                    botonFacil.enabled = true;
                    botonMedio.enabled = true;
                    botonDificil.enabled = true;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (minigame.GetMinigameScore())
            {
                case MinigameScore.None:
                    botonFacil.enabled = true;
                    botonMedio.enabled = false;
                    botonDificil.enabled = false;

                    estrellaVacia.SetActive(true);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Bronze:
                    botonFacil.enabled = true;
                    botonMedio.enabled = true;
                    botonDificil.enabled = false;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(true);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Silver:
                    botonFacil.enabled = true;
                    botonMedio.enabled = true;
                    botonDificil.enabled = true;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(true);
                    estrellaOro.SetActive(false);
                    break;
                case MinigameScore.Gold:
                    botonFacil.enabled = true;
                    botonMedio.enabled = true;
                    botonDificil.enabled = true;

                    estrellaVacia.SetActive(false);
                    estrellaBronce.SetActive(false);
                    estrellaPlata.SetActive(false);
                    estrellaOro.SetActive(true);
                    break;
            }
        }
    }

    public void StartMinigame(string dif)
    {
        switch (dif)
        {
            case "facil":
                GameManager.instance.SetCurrentDifficulty(Dificultad.Facil);
                GameManager.instance.StartTransition();
                minigame.StartMinigame();
                break;
            case "medio":
                GameManager.instance.SetCurrentDifficulty(Dificultad.Medio);
                GameManager.instance.StartTransition();
                minigame.StartMinigame();
                break;
            case "dificil":
                GameManager.instance.SetCurrentDifficulty(Dificultad.Dificil);
                GameManager.instance.StartTransition();
                minigame.StartMinigame();
                break;
            default:
                print("Error en la dificultad");
                break;
        }
        HideInfoCanvas();
    }
}