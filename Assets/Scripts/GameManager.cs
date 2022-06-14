using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] Dificultad currentGameDifficulty;
    [SerializeField] Minijuego currentMinigame;

    [SerializeField] Object overworld; //Mapa del pueblo

    public Transform playerTransform; //Para saber donde está el player al volver al overworld

    Object scene = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentDifficulty(Dificultad dif)
    {
        currentGameDifficulty = dif;
    }

    public Dificultad GetCurrentGameDifficulty()
    {
        return currentGameDifficulty;
    }

    public void SetCurrentMinigame(Minijuego min)
    {
        currentMinigame = min;
    }

    public Minijuego GetCurrentMinigame()
    {
        return currentMinigame;
    }

    public void OnWinGame()
    {
        switch (currentGameDifficulty)
        {
            case Dificultad.Facil:
                if(currentMinigame.GetMinigameScore() == MinigameScore.None)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Bronze);
                }
                else
                {
                    print("nada");
                }
                break;
            case Dificultad.Medio:
                if(currentMinigame.GetMinigameScore() == MinigameScore.Bronze)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Silver);
                }
                break;
            case Dificultad.Dificil:
                if(currentMinigame.GetMinigameScore() == MinigameScore.Silver)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Gold);
                }
                break;
        }

        LoadOverworld();
    }

    public void OnLoseGame()
    {
        LoadOverworld();
    }

    public void LoadScene(Object scene)
    {
        //playerTransform = FindObjectOfType<PlayerController>().transform;
        //print(playerTransform.position);
        this.scene = scene;
        SceneManager.LoadScene(scene.name);
    }

    public void LoadOverworld()
    {
        SceneManager.LoadScene(overworld.name);
    }

}
