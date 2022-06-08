using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minijuego : MonoBehaviour
{
    [SerializeField] Object minigameScene;
    [SerializeField] MinigameScore gameScore = MinigameScore.None;
    [SerializeField] Canvas enterCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(minigameScene.name);
    }

    public void SetMinigameScore(MinigameScore score)
    {
        this.gameScore = score;
    }

    public void ShowCanvas()
    {
        enterCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        enterCanvas.gameObject.SetActive(false);
    }
}

public enum MinigameScore
{
    None, Bronze, Silver, Gold
}
