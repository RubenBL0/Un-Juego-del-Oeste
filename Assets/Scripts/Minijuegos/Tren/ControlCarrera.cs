using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCarrera : MonoBehaviour
{
    public static event Action trenAlcanzado;
    public static event Action trenSaliendo;
    private Rigidbody2D rb;
    private bool controlTecla, tiempoAgotado;
    [SerializeField] private float fuerzaEmpuje;

    private void OnEnable()
    {
        TrenGameManager.tiempoAgotado += TiempoAgotado;
    }
    private void OnDisable()
    {
        TrenGameManager.tiempoAgotado -= TiempoAgotado;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tiempoAgotado = false;
    }
    void Update()
    {
        float velocidad;

        if (Input.GetKeyDown(KeyCode.A) && controlTecla && !tiempoAgotado)
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && !controlTecla && !tiempoAgotado)
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = true;
        }
        //tecla para probar movimiento sin tener que jugar todo el tiempo
        if (Input.GetKey(KeyCode.Q) && !tiempoAgotado)
        {
            rb.AddForce(rb.transform.right * 0.1f, ForceMode2D.Impulse);
        }

        velocidad = rb.velocity.magnitude;
        if (velocidad > 50)
        {
            velocidad = 50;
            rb.velocity = new Vector2(velocidad, 0);
        }
        //Debug.Log(rb.velocity.magnitude);
    }
    void TiempoAgotado()
    {
        tiempoAgotado = true;
        StartCoroutine(ReduceVelocidad());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "tren") trenAlcanzado?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "tren") trenSaliendo?.Invoke();
    }
    IEnumerator ReduceVelocidad()
    {
        while (true)
        {
            float velocidad = rb.velocity.magnitude;
            rb.velocity = new Vector2(velocidad - 0.01f, 0);
            yield return new WaitForSeconds(0.1f);
            if (velocidad <= 1) break;
        }
        //Debug.Log("fin corrutina");
    }
}
