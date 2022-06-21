using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Configurables")]
    [SerializeField] private float vidaMax;
    [SerializeField] private int dano;
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField, Range(0f, 10f)] private int fuerzaLanzamieto;
    [SerializeField] private float lerpDuration;
    [SerializeField] private LayerMask enemigoLayer, recogibleLayer;
    [SerializeField] private AnimationCurve curve;
    [Space(20f)]

    [SerializeField] private float vida;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D rbBotella;
    [SerializeField] private Image barraVida;
    [SerializeField] private Transform posBotella;
    [SerializeField] private AudioSource golpe, recibeGolpe, lanzaBotella;
    [SerializeField] private Animator animator;

    private float vh;
    private float vv;
    private bool recogible;
    
    private Vector2 playerInput;

    public delegate void Del_RetornaPlayer(GameObject go);
    public static event Del_RetornaPlayer retornaPlayer;
    public static event Action muerte;

    #region Suscribe Eventos
    private void OnEnable()
    {
        PeleaManager.instanciaEnemigos += RetornaPlayer;
    }
    private void OnDisable()
    {
        PeleaManager.instanciaEnemigos -= RetornaPlayer;
    }
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vida = vidaMax;
        recogible = false;
    }

    // Update is called once per frame
    void Update()
    {
        vh = Input.GetAxisRaw("Horizontal");
        vv = Input.GetAxisRaw("Vertical");
        playerInput = new Vector2(vh, vv).normalized;
        CompruebaMirada();
        LanzaBotella();
        RaycastPlayer();        

        if (vida <= 0)
        {
            muerte?.Invoke();
        }
    }    

    private void FixedUpdate()
    {
        MuevePlayer();
    }

    void ActualizaBarraVida()
    {
        float currentFillAmount = barraVida.fillAmount;
        StartCoroutine(LerpBarraVida(currentFillAmount, (vida / vidaMax), lerpDuration));
    }

    private void RaycastPlayer()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 2f, enemigoLayer);

            animator.SetTrigger("Pega");

            if (hit.collider != null)
            {
                golpe.Play();
                hit.transform.GetComponent<Enemigo>().RestaVida(dano);
            }
        }
        if (Input.GetKeyDown(KeyCode.O) && !recogible)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 2f, recogibleLayer);

            if (hit.collider != null)
            {
                rbBotella = hit.transform.GetComponent<Rigidbody2D>();
                rbBotella.simulated = false;
                hit.transform.SetParent(posBotella);
                hit.transform.localPosition = Vector3.zero;
                hit.transform.localRotation = Quaternion.identity;
                recogible = true;
            }
        }
    }

    void LanzaBotella()
    {
        if (Input.GetKeyDown(KeyCode.O) && recogible)
        {
            rbBotella.transform.SetParent(null);
            rbBotella.simulated = true;
            rbBotella.AddForce(transform.right * fuerzaLanzamieto, ForceMode2D.Impulse);
            lanzaBotella.Play();
            recogible = false;
        }
    }

    void MuevePlayer()
    {
        rb.MovePosition(rb.position + playerInput * speed * Time.fixedDeltaTime);
    }

    void RetornaPlayer()
    {
        retornaPlayer?.Invoke(this.gameObject);
    }

    public int GetDano()
    {
        return dano;
    }

    public void RestaVida(int a)
    {
        recibeGolpe.Play();
        vida -= a;
        ActualizaBarraVida();
    }

    void CompruebaMirada()
    {
        if (vh > 0f) //derecha
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 12f * Time.deltaTime);
        }
        if (vh < 0f) //izquierda
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, 180f), 12f * Time.deltaTime);
        }

        if (vv > 0f) //arriba
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, 90f), 12f * Time.deltaTime);
        }
        if (vv < 0f) //abajo
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, -90f), 12f * Time.deltaTime);
        }

        if (vh > 0f && vv > 0f) //derecha arriba
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, 45f), 12f * Time.deltaTime);
        }
        if (vh > 0f && vv < 0f) //derecha abajo
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, -45f), 12f * Time.deltaTime);
        }

        if (vh < 0f && vv > 0f) //izquierda arriba
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, 135f), 12f * Time.deltaTime);
        }
        if (vh < 0f && vv < 0f) //izquierda abajo
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0f, 0f, -135f), 12f * Time.deltaTime);
        }
    }

    IEnumerator LerpBarraVida(float start, float target, float lerpDuration)
    {
        float timeElapsed = 0f;
        float current;

        while (timeElapsed < lerpDuration)
        {
            current = Mathf.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            barraVida.fillAmount = current;
            timeElapsed +=Time.deltaTime;
            yield return null;
        }
        barraVida.fillAmount = target;
    }
}
