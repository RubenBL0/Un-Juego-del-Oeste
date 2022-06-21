using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spittle : MonoBehaviour
{
    [SerializeField] Transform bocaTransform;
    [SerializeField] AudioSource escupitajo;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spit(float power)
    {        
        gameObject.SetActive(true);
        escupitajo.Play();
        rb.AddForce(new Vector2(-power, 7f), ForceMode2D.Impulse);        
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        transform.position = bocaTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Suelo")
        {
            EscupitajoManager.instance.Restart();
            print("suelo");
        }
        if (other.tag == "Caldero")
        {
            EscupitajoManager.instance.AddScore(1);
            EscupitajoManager.instance.Restart();
            print("caldero");
        }
    }
}
