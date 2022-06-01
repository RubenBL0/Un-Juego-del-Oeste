using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DueloManager : MonoBehaviour
{
    bool gameStarted = true;
    Coroutine gameCoroutine = null;

    [SerializeField, Range(1f, 5f)] float minTime = 3f;
    [SerializeField, Range(6f, 15f)] float maxTime = 10f;
    float duelTime; //Tiempo que va a durar el duelo (aleatorio)
    [SerializeField, Range(0.5f, 2f)] float stepTime; //Tiempo para dar cada paso

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    [SerializeField] Canvas canvasFire;
    [SerializeField] TextMeshProUGUI txtKey;

    // Start is called before the first frame update
    void Start()
    {
        print(KeyCode.A.ToString());
        canvasFire.gameObject.SetActive(false);
        duelTime = GetDuelTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted && gameCoroutine == null)
        {
            gameCoroutine = StartCoroutine(TimeCoroutine());
        }        
    }

    float GetDuelTime()
    {
        float time = Random.Range(minTime, maxTime);
        return time;
    }

    IEnumerator TimeCoroutine()
    {
        while (duelTime > 0)
        {
            if (stepTime < duelTime)
            {
                duelTime -= stepTime;
                MoveCharacters();
                yield return new WaitForSeconds(stepTime);
            }
            if(stepTime > duelTime)
            {
                yield return new WaitForSeconds(duelTime);
                ShowMessage();
                //Disparar
                duelTime = 0f;
            }
        }
        yield return null;
    }

    void MoveCharacters()
    {
        player.transform.position = new Vector2(player.transform.position.x - 0.5f, player.transform.position.y);
        enemy.transform.position = new Vector2(enemy.transform.position.x + 0.5f, enemy.transform.position.y);
    }

    void ShowMessage()
    {
        char c = RandomKey();
        canvasFire.gameObject.SetActive(true);
        txtKey.SetText(c.ToString());

        CheckKey(c);
    }

    char RandomKey()
    {
        string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char c = st[Random.Range(0, st.Length)];

        return c;
    }

    void CheckKey(char c)
    {
        Input.GetButton()
    }
}
