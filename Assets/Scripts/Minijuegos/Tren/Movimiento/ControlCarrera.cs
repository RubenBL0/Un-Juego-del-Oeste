using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCarrera : MonoBehaviour
{
    public static event Action trenAlcanzado;
    public static event Action trenSaliendo;
    private Rigidbody2D rb;
    private bool controlTecla, trenEscapado;
    [SerializeField] private float fuerzaEmpuje, fuerzaSalto;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource carreraCaballo, piedra;

    private void OnEnable()
    {
        TrenGameManager.escapaTren += TrenEscapado;
        TrenGameManager.tiempoAlcanzado += TiempoAlcanzado;
    }
    private void OnDisable()
    {
        TrenGameManager.escapaTren -= TrenEscapado;
        TrenGameManager.tiempoAlcanzado -= TiempoAlcanzado;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        carreraCaballo.Play();
        trenEscapado = false;
    }
    void Update()
    {        
        MuevePlayer();
        ControlVelocidad();
        //Debug.Log(Vector2.Distance(transform.position, prueba.transform.position)); //distancia 6.7
    }

    private void ControlVelocidad()
    {
        float velX;
        float velY;

        velX = rb.velocity.x;
        velY = rb.velocity.y;
        if (velX > 16)
        {
            velX = 16;
            rb.velocity = new Vector2(velX, rb.velocity.y);
        }
        if (velY > 16)
        {
            velY = 16;
            rb.velocity = new Vector2(rb.velocity.x, velY);
        }
    }

    bool IsGrounded()
    {
        Vector2 posicion = transform.position;
        Vector2 direccion = Vector2.down;
        float distancia = 1.2f;

        RaycastHit2D hit = Physics2D.Raycast(posicion, direccion, distancia, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    private void MuevePlayer()
    {
        if (Input.GetKeyDown(KeyCode.G) && controlTecla && !trenEscapado && IsGrounded())
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = false;
        }
        if (Input.GetKeyDown(KeyCode.H) && !controlTecla && !trenEscapado && IsGrounded())
        {
            rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
            controlTecla = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !trenEscapado && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(transform.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        //tecla para probar movimiento sin tener que jugar todo el tiempo
        /*if (Input.GetKey(KeyCode.Q) && !trenEscapado && IsGrounded())
        {
            rb.AddForce(rb.transform.right * 0.1f, ForceMode2D.Impulse);
        }*/
    }
    void TrenEscapado()
    {
        trenEscapado = true;
        StartCoroutine(ReduceVelocidad());
    }
    void TiempoAlcanzado()
    {
        //Debug.Log("Tiempo alcanzado, se reduce potencia");
        StartCoroutine(ReducePotencia());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "tren") trenAlcanzado?.Invoke();
        if (collision.transform.tag == "obstaculo")
        {
            //Debug.Log("obstaculo detectado");
            piedra.Play();
            rb.AddForce(-transform.right * 8, ForceMode2D.Impulse);
        }
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
    IEnumerator ReducePotencia()
    {
        while (true)
        {
            fuerzaEmpuje -= 0.1f;
            if (fuerzaEmpuje <= 0.6f) 
            {
                fuerzaEmpuje = 0.6f;
                break;
            }
            yield return new WaitForSeconds(45f);
        }
    }
}
