using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private DatosEnemigo datosEnemigo;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemigoGolpe controlGolpe;

    public static event Action sumaEnemigos;
    public static event Action restaEnemigos;

    private int vida;
    private float speed;
    private float probRage;
    private bool rage;

    Vector2 playerDir;
    
    #region Suscribe Eventos
    private void OnEnable()
    {
        Player.retornaPlayer += GetPlayer;
    }
    private void OnDisable()
    {
        Player.retornaPlayer -= GetPlayer;
    }
    #endregion

    private void Awake()
    {
        vida = datosEnemigo.Vida;
        speed = datosEnemigo.Speed;  
        probRage = datosEnemigo.ProbRage;
        rb = GetComponent<Rigidbody2D>();
        sumaEnemigos?.Invoke();
        rage = false;
    }

    private void Start()
    {
        StartCoroutine(Rage(probRage));
    }

    void Update()
    {
        playerDir = player.transform.position - rb.transform.position;
        transform.right = playerDir.normalized;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerDir.normalized * speed * Time.fixedDeltaTime);
    }

    public void Muerte()
    {
        restaEnemigos?.Invoke();
        Destroy(this.gameObject);
    }

    void GetPlayer(GameObject go)
    {
        player = go;
    }
    public void RestaVida(int a)
    {
        vida -= a;
        Debug.Log(vida);
    }  
    
    IEnumerator Rage(float probRage)
    {
        while (!rage)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= probRage) rage = true;
            yield return new WaitForSeconds(5f);
        }
        speed += 1f;
        controlGolpe.dano += 1;
        controlGolpe.frecuenciaAtaque -= 0.5f;
        vida += player.GetComponent<Player>().GetDano();
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
