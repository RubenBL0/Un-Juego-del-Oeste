using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vaquillas : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField, Range(0f, 5f)] private float speed;

    private float mh;
    private float mv;

    public bool atrapada;
    
    private Vector2 vaquillaInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        atrapada = false;
        StartCoroutine(MueveVaquilla());
    }
    public void Atrapada()
    {
        atrapada = true;
        rb.velocity = Vector2.zero; 
        rb.angularVelocity = 0f;
    }
    public void Liberada()
    {
        atrapada = false;
        StartCoroutine(MueveVaquilla());
    }

    IEnumerator MueveVaquilla()
    {
        while (!atrapada)
        {
            mh = Random.Range(-10f, 10f);
            mv = Random.Range(-10f, 10f);

            Vector2 moveVaquilla = rb.position;

            vaquillaInput = new Vector2(mh, mv);
            vaquillaInput = Vector2.ClampMagnitude(vaquillaInput, 1);

            rb.AddForce(vaquillaInput * speed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
