using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] private float fuerzaEmpuje;
    public float limVelocidad;
    private Rigidbody2D rb;
    private int vecesAlcanzado;
    public bool controlCabina;
    private void OnEnable()
    {
        ControlCarrera.trenAlcanzado += AumentaVelocidad;
    }
    private void OnDisable()
    {
        ControlCarrera.trenAlcanzado -= AumentaVelocidad;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fuerzaEmpuje = 1f;
        limVelocidad = 35f;
        vecesAlcanzado = -1;
        controlCabina = true;
    }
    private void Start()
    {
        InvokeRepeating("MueveTren", 1f, 0.1f);
    }
    private void Update()
    {
        float velocidad;
        velocidad = rb.velocity.magnitude;
        if (velocidad > limVelocidad)
        {
            velocidad = limVelocidad;
            rb.velocity = new Vector2(velocidad, 0);
        }
    }
    void MueveTren()
    {
        rb.AddForce(rb.transform.right * fuerzaEmpuje, ForceMode2D.Impulse);
    }
    void AumentaVelocidad()
    {
        if (controlCabina)
        {
            //Debug.Log("aumentando velocidad");
            vecesAlcanzado++;
            switch (vecesAlcanzado)
            {
                case 0:
                    limVelocidad = 40f;
                    break;

                case 1:
                    limVelocidad = 45f;
                    break;

                case 2:
                    limVelocidad = 48f;
                    break;

                default:
                    break;
            }
        }        
    }
}
