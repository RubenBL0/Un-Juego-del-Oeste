using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego : MonoBehaviour
{
    [SerializeField] Object minigameScene;

    [SerializeField] MinigameScore gameScore = MinigameScore.None;

    public MinijuegoTrigger trigger;

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
            print("destruir");
            Destroy(this.transform.parent.gameObject);
        }
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
        GameManager.instance.LoadScene(minigameScene);
        GameManager.instance.SetCurrentMinigame(this);
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
