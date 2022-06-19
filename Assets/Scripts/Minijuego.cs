using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego : MonoBehaviour
{
    [SerializeField] Object minigameScene;

    [SerializeField] MinigameNames name;
    [SerializeField] MinigameScore gameScore = MinigameScore.None;

    public MinijuegoTrigger trigger;

    public bool requiresStars = false; //Marcar esto si requiere las estrellas para entrar

    public static GameObject minigamesParent = null;

    private void Awake()
    {
        if(minigamesParent == null)
        {
            DontDestroyOnLoad(this.transform.parent.gameObject);
            minigamesParent = this.transform.parent.gameObject;
        }
        else if(minigamesParent != this.transform.parent.gameObject)
        {
            Destroy(this.transform.parent.gameObject);
        }

    }

    private void OnEnable()
    {
        gameScore = DataManager.instance.GetMinigameScore(name);
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMinigameScore(MinigameScore score)
    {
        this.gameScore = score;
    }
    public MinigameScore GetMinigameScore()
    {
        return gameScore;
    }

    public void StartMinigame()
    {
        //GameManager.instance.StartTransition();
        //Invoke("LoadMinigame", 1f);
        Invoke("LoadMinigame", 1f);
        GameManager.instance.SetCurrentMinigame(this, name);
    }

    void LoadMinigame()
    {
        GameManager.instance.LoadScene(minigameScene);
    }

}
public enum MinigameScore
{
    None, Bronze, Silver, Gold
}
public enum MinigameNames
{
    Vacas, Duelo, Latas, Pelea, Escupitajo, Tren
}
