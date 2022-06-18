using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] Dificultad currentGameDifficulty;
    [SerializeField] Minijuego currentMinigame;
    [SerializeField] MinigameNames currentMinigameName;

    [SerializeField] Object overworld; //Mapa del pueblo

    [SerializeField] private Animator sceneAnimator;
    [SerializeField] private GameObject sceneLoadManager;
    
    [SerializeField] PlayerController player;

    public Canvas pauseMenu;
    public Canvas pauseMinigame;

    public bool isPaused = false;

    public AudioSource musicaPueblo;

    Object scene = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().transform.position = DataManager.instance.GetPosition();
        musicaPueblo = GetComponent<AudioSource>();
        musicaPueblo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void SetCurrentDifficulty(Dificultad dif)
    {
        currentGameDifficulty = dif;
    }

    public Dificultad GetCurrentGameDifficulty()
    {
        return currentGameDifficulty;
    }

    public void SetCurrentMinigame(Minijuego min, MinigameNames name)
    {
        currentMinigame = min;
        currentMinigameName = name;
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
                if (currentMinigame.GetMinigameScore() == MinigameScore.None)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Bronze);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 1);
                }
                else
                {
                    print("nada");
                }
                break;
            case Dificultad.Medio:
                if (currentMinigame.GetMinigameScore() == MinigameScore.Bronze)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Silver);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 2);
                }
                break;
            case Dificultad.Dificil:
                if (currentMinigame.GetMinigameScore() == MinigameScore.Silver)
                {
                    currentMinigame.SetMinigameScore(MinigameScore.Gold);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 3);
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
        DataManager.instance.SavePosition(FindObjectOfType<PlayerController>().transform.position);
        ActivatePlayer(false);
        musicaPueblo.Stop();
    }

    public void StartTransition()
    {
        if (sceneLoadManager == null)
        {
            sceneLoadManager = FindObjectOfType<Fade>(true).transform.parent.gameObject;
            sceneAnimator = FindObjectOfType<Fade>(true).GetComponent<Animator>();
        }
        sceneLoadManager.SetActive(true);
        sceneAnimator.SetTrigger("StartTransition");
    }

    public void LoadOverworld()
    {
        currentMinigame = null;
        SceneManager.LoadScene(overworld.name);
        PlayerController.instance.transform.position = DataManager.instance.GetPosition();
        ActivatePlayer(true);
        musicaPueblo.Play();
    }

    public void ReturnToMainMenu()
    {
        if (PlayerController.instance.gameObject.activeSelf)
        {
            DataManager.instance.SavePosition(PlayerController.instance.transform.position);
        }
        DataManager.instance.SaveData();
        Destroy(FindObjectOfType<Minijuego>().transform.parent.gameObject);
        Destroy(FindObjectOfType<MinijuegoTrigger>().transform.parent.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivatePlayer(bool status)
    {
        PlayerController.instance.gameObject.SetActive(status);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if(currentMinigame == null) //En el pueblo
        {
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            pauseMinigame.gameObject.SetActive(true);
        }
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
        pauseMinigame.gameObject.SetActive(false);

        isPaused = false;
    }
}
