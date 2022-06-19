using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaController : MonoBehaviour
{
    [SerializeField] private TrainController tren;
    [SerializeField] private Animator animPuerta;
    [SerializeField] private AudioSource sirena, relinchaCaballo;
    private bool controlCorrutina;
    private int vecesAlcanzado;

    private void Awake()
    {
        controlCorrutina = true;
        vecesAlcanzado = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("Player Detectado");            

            if (controlCorrutina)
            {
                sirena.Play();
                vecesAlcanzado++;
                controlCorrutina = false;
                if (vecesAlcanzado <= 3) StartCoroutine(EscapaDelPlayer());                
            }
        }
    }
    IEnumerator EscapaDelPlayer()
    {
        tren.limVelocidad = 20f;
        yield return new WaitForSeconds(5f);

        switch (vecesAlcanzado)
        {
            case 1:
                tren.limVelocidad = 11f;
                break;

            case 2:
                tren.limVelocidad = 12f;
                break;

            case 3:
                animPuerta.SetTrigger("AbrirPuerta");
                relinchaCaballo.Play();
                tren.limVelocidad = 14f;
                break;

            default:
                break;
        }

        controlCorrutina = true;
    }
}
