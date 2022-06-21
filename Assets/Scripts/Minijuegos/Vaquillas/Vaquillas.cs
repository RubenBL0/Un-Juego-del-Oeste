using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vaquillas : MonoBehaviour
{
    public delegate void DelegateVaquilla();
    public static event DelegateVaquilla establoEntrando;
    public static event DelegateVaquilla establoSaliendo;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource sonidoVaca, sonidoCuerda, sonidoTirarCuerda;
    [SerializeField, Range(0f, 5f)] private float speed;

    public lr_LineController linea;    

    private float mh;
    private float mv;

    public bool atrapada;
    
    private Vector2 vaquillaInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        linea = FindObjectOfType<lr_LineController>();
        atrapada = false;
        StartCoroutine(MueveVaquilla());
    }
    public void Atrapada()
    {
        atrapada = true;
        rb.velocity = Vector2.zero; 
        rb.angularVelocity = 0f;
        linea.lr.enabled = true;
        linea.lr_points[0] = gameObject.transform;
        sonidoCuerda.Play();
        sonidoTirarCuerda.Play();
    }
    public void Liberada()
    {
        atrapada = false;
        linea.lr.enabled = false;
        linea.lr_points[0] = null;
        StartCoroutine(MueveVaquilla());
        sonidoTirarCuerda.Stop();
        if (Random.Range(0f, 1f) >= 0.5f) sonidoVaca.Play();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "establo")
        {
            establoEntrando();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "establo")
        {
            establoSaliendo();
        }
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
