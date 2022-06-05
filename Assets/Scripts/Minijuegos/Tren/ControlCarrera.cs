using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCarrera : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool controlTecla;
    [SerializeField] private float fuerzaEmpuje;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
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
    }
}
