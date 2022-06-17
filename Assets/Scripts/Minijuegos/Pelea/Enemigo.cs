using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private DatosEnemigo datosEnemigo;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;

    public static event Action sumaEnemigos;
    public static event Action restaEnemigos;

    private int vida;
    private float speed;
    private int dano;
    private float frecuenciaAtaque;

    private bool controlAtaque;

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
        dano = datosEnemigo.Dano;
        controlAtaque = false;
        frecuenciaAtaque = datosEnemigo.FrecuenciaAtaque;
        rb = GetComponent<Rigidbody2D>();
        sumaEnemigos?.Invoke();
    }

    void Update()
    {
        playerDir = player.transform.position - rb.transform.position;
        transform.right = playerDir.normalized;

        if (vida <= 0)
        {
            restaEnemigos?.Invoke();
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerDir.normalized * speed * Time.fixedDeltaTime);
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
    void Ataca()
    {
        player.transform.GetComponent<Player>().RestaVida(dano);
        controlAtaque = true;
        StartCoroutine(DelayEntreAtaques());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!controlAtaque) Ataca();
        }
    }

    IEnumerator DelayEntreAtaques()
    {
        yield return new WaitForSeconds(frecuenciaAtaque);
        controlAtaque = false;
    }
}
