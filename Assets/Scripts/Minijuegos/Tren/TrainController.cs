using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] private float fuerzaEmpuje;
    public float limVelocidad;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fuerzaEmpuje = 1f;
        limVelocidad = 35f;
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
}
