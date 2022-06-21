using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lata : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField, Range(0f, 10f)] private int fuerza;
    [SerializeField] private Sprite altSprite;
    [SerializeField] private GameObject animDisparo;
    [SerializeField] private AudioSource impactoLata;

    public static event Action diana;
    public static event Action sueloTocado;

    private int cuentaLatas;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cuentaLatas = 0;
    }
    private void OnMouseDown()
    {
        SoundManager.PlaySound(impactoLata);
        Instantiate(animDisparo, GetMousePos(), Quaternion.identity);
        cuentaLatas++;
        ActualizaSprite(altSprite);
        MueveLata();
        diana?.Invoke();
    }
    void ActualizaSprite(Sprite alsprite)
    {
        if(cuentaLatas > 3)
        {
            GetComponent<SpriteRenderer>().sprite = alsprite;
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Suelo") sueloTocado?.Invoke();
    }
}
