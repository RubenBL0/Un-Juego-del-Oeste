using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public Canvas optionsMenu;
    private Canvas lastCanvas;

    private bool isPaused = false;
    private bool isOptionsMenuShown = false;
    public bool isMinigameCanvasShown = false;

    public MinigameScore minigamesStatus = MinigameScore.None; //Se guarda el valor minimo de las estrellas

    AudioSource musicaPueblo;

    public Slider volumeSlider;
    public TextMeshProUGUI txtVolume;

    public int final; //Del 0 al 4, los diferentes finales que hay

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
        volumeSlider.value = DataManager.instance.Volume() * 100;
        txtVolume.text = volumeSlider.value.ToString();
        FindObjectOfType<PlayerController>().transform.position = DataManager.instance.GetPosition();
        musicaPueblo = GetComponent<AudioSource>();
        PlayMusic();
        CheckStarStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            OnWinGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMinigameCanvasShown)
            {
                PlayerController.instance.trigger.HideInfoCanvas();
            }
            else if (isPaused)
            {
                if (isOptionsMenuShown)
                {
                    HideOptionsMenu();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PlayMusic()
    {
        musicaPueblo.Play();
        musicaPueblo.volume = DataManager.instance.Volume();
    }

    public void StopMusic()
    {
        musicaPueblo.Stop();
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
        bool getstar = false;
        switch (currentGameDifficulty)
        {
            case Dificultad.Facil:
                if (currentMinigame.GetMinigameScore() == MinigameScore.None)
                {
                    getstar = true;
                    StarManager.instance.WinGameWithStar(Dificultad.Facil);
                    currentMinigame.SetMinigameScore(MinigameScore.Bronze);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 1);
                }
                break;
            case Dificultad.Medio:
                if (currentMinigame.GetMinigameScore() == MinigameScore.Bronze)
                {
                    getstar = true;
                    StarManager.instance.WinGameWithStar(Dificultad.Medio);
                    currentMinigame.SetMinigameScore(MinigameScore.Silver);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 2);
                }
                break;
            case Dificultad.Dificil:
                if (currentMinigame.GetMinigameScore() == MinigameScore.Silver)
                {
                    getstar = true;
                    StarManager.instance.WinGameWithStar(Dificultad.Dificil);
                    currentMinigame.SetMinigameScore(MinigameScore.Gold);
                    DataManager.instance.SaveMinigameScore(currentMinigameName, 3);
                }
                break;
        }

        if (!getstar)
        {
            Invoke("LoadOverworld", 2f);
        }
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
        CheckStarStatus();
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
            lastCanvas = pauseMenu;
        }
        else
        {
            pauseMinigame.gameObject.SetActive(true);
            lastCanvas = pauseMinigame;
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

    public void OnVolumeChange()
    {
        int volume = (int)volumeSlider.value;
        txtVolume.text = volumeSlider.value.ToString();
        DataManager.instance.SetVolume(volume);
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.volume = DataManager.instance.Volume();
        }
    }

    public void ShowOptionsMenu()
    {
        lastCanvas.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        isOptionsMenuShown = true;
    }

    public void HideOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(false);
        lastCanvas.gameObject.SetActive(true);
        isOptionsMenuShown = false;
    }

    public void CheckStarStatus() //Chequea el valor minimo de estrellas del jugador y lo escribe en la variable minigameStatus
    {
        int none = 0, bronze = 0, silver = 0, gold = 0;
        foreach(Minijuego minijuego in FindObjectsOfType<Minijuego>(true))
        {
            if (!minijuego.requiresStars)
            {
                none = minijuego.GetMinigameScore() == MinigameScore.None ? ++none : none;
                bronze = minijuego.GetMinigameScore() == MinigameScore.Bronze ? ++bronze : bronze;
                silver = minijuego.GetMinigameScore() == MinigameScore.Silver ? ++silver : silver;
                gold = minijuego.GetMinigameScore() == MinigameScore.Gold ? ++gold : gold;
            }
        }
        minigamesStatus = none != 0 ? MinigameScore.None : (bronze != 0 ? MinigameScore.Bronze : (silver != 0 ? MinigameScore.Silver : MinigameScore.Gold));
    }

    public void OnWinFinalGame(int final)
    {
        this.final = final;
        SceneManager.LoadScene("Finales");
    }

    public void OnDieFinalGame()
    {
        final = 0;
        SceneManager.LoadScene("Finales");
    }

    public void OnLoseFinalGame()
    {
        final = 1;
        SceneManager.LoadScene("Finales");
    }
}
