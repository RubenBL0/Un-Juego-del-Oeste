using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lata : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField, Range(0f, 10f)] private int fuerza;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        MueveLata();
    }

    private void MueveLata()
    {
        Vector2 direccion = transform.position - GetMousePos();
        direccion.x = Mathf.Clamp(direccion.x, -0.1f, 0.1f);
        if (direccion.y < 0.1f) direccion.y = direccion.y = 0.1f;
        if (Mathf.Abs(direccion.x) > direccion.y - 0.1f) direccion.y = Mathf.Abs(direccion.x) + 0.2f;

        float fgiro = Random.Range(-10f, 10f);
        float velX = Mathf.Clamp(rb.velocity.x, -3f, 3f);
        float velY = Mathf.Clamp(rb.velocity.y, -3f, 3f);

        rb.velocity = new Vector2(velX, velY);
        rb.AddForce(direccion.normalized * fuerza, ForceMode2D.Impulse);
        rb.AddTorque(fgiro);
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
