using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGolpe : MonoBehaviour
{
    [SerializeField] private DatosEnemigo datosEnemigo;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource golpe;
    [SerializeField] private Enemigo enemigo;

    private bool controlAtaque;
    public int dano;
    public float frecuenciaAtaque;

    private void Awake()
    {        
        dano = datosEnemigo.Dano;
        controlAtaque = false;
        frecuenciaAtaque = datosEnemigo.FrecuenciaAtaque;
    }
    void Ataca()
    {
        enemigo.animator.SetTrigger("Pega");
        golpe.Play();
        player.transform.GetComponent<Player>().RestaVida(dano);
        controlAtaque = true;
        StartCoroutine(DelayEntreAtaques());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            if (!controlAtaque) Ataca();
        }
    }

    IEnumerator DelayEntreAtaques()
    {
        yield return new WaitForSeconds(frecuenciaAtaque);
        controlAtaque = false;
    }
}
