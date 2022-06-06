using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCarrera : MonoBehaviour
{
    public static event Action trenAlcanzado;
    private Rigidbody2D rb;
    private bool controlTecla;
    [SerializeField] private float fuerzaEmpuje;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float velocidad;

        if (Input.GetKeyDown(KeyCode.A) && controlTecla)
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && !controlTecla)
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = true;
        }

        velocidad = rb.velocity.magnitude;
        if (velocidad > 50)
        {
            velocidad = 50;
            rb.velocity = new Vector2(velocidad, 0);
        }
        //Debug.Log(rb.velocity.magnitude);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "tren") trenAlcanzado?.Invoke();
    }
}
