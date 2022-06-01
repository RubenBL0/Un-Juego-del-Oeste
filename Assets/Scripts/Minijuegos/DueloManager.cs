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
    [SerializeField, Range(0.5f, 2f)] float fireTime; //Tiempo para disparar antes que el enemigo

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    [SerializeField] Canvas canvasFire;
    [SerializeField] TextMeshProUGUI txtKey;

    //Animators
    Animator playerAnim;
    Animator enemyAnim;

    //Health
    [SerializeField] int playerHealth = 3;
    [SerializeField] int enemyHealth = 3;

    //Corazones
    [SerializeField] GameObject[] playerHearts = new GameObject[3];
    [SerializeField] GameObject[] enemyHearts = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        canvasFire.gameObject.SetActive(false);
        duelTime = GetDuelTime();

        playerAnim = player.GetComponent<Animator>();
        enemyAnim = enemy.GetComponent<Animator>();
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
                //Disparar
                StartFire(); //Sale el mensaje en pantalla y empieza el timer
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

    void StartFire()
    {
        char c = RandomKey();
        canvasFire.gameObject.SetActive(true);
        txtKey.SetText(c.ToString());

        StartCoroutine(CheckKey(c));
    }

    char RandomKey()
    {
        string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char c = st[Random.Range(0, st.Length)];

        return c;
    }

    IEnumerator CheckKey(char c)
    {
        float currentTime = 0f;
        while (currentTime < fireTime)
        {
            currentTime += Time.deltaTime;

            KeyCode myKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), c.ToString());
            if (Input.GetKeyDown(myKeyCode))
            {
                print("ganaste");
                WinDuel();
                yield break;
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    print("moriste");
                    LoseDuel();
                    yield break;

                }
            }
            yield return null;
        }
        print("TIEMPO!");
        LoseDuel();
        yield return null;
    }

    void WinDuel()
    {
        enemyHealth -= 1;
        playerAnim.SetTrigger("Shooting");
        enemyAnim.SetTrigger("Dead");
        CheckGameState();
    }

    void LoseDuel()
    {
        playerHealth -= 1;
        enemyAnim.SetTrigger("Shooting");
        playerAnim.SetTrigger("Dead");
        CheckGameState();
    }

    void CheckGameState()
    {
        UpdateHealth();
        if(playerHealth <= 0)
        {
            LoseGame();
        }
        if(enemyHealth <= 0)
        {
            WinGame();
        }
    }

    //Cambiar los corazones
    void UpdateHealth()
    {

    }
    void LoseGame()
    {
        print("Has perdido...");
    }

    void WinGame()
    {
        print("¡Has ganado!");
    }
}
