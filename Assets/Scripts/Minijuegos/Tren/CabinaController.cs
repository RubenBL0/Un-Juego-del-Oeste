using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinaController : MonoBehaviour
{
    [SerializeField] private TrainController tren;
    private float prevLimVelocidad;
    private bool controlCorrutina;

    private void Awake()
    {
        controlCorrutina = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Player Detectado");            

            if (controlCorrutina)
            {
                tren.controlCabina = false;
                prevLimVelocidad = tren.limVelocidad;
                StartCoroutine(EscapaDelPlayer(prevLimVelocidad));
                controlCorrutina = false;
            }
        }
    }
    IEnumerator EscapaDelPlayer(float a)
    {
        tren.limVelocidad = 60f;
        yield return new WaitForSeconds(5f);
        tren.limVelocidad = a;
        controlCorrutina = true;
        tren.controlCabina = true;
    }
}
