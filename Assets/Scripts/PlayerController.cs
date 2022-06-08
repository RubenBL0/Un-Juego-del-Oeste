using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField, Range(0f, 50f)] private float speed;

    private float mh;
    private float mv;
    
    private Vector2 playerInput;

    private Minijuego minijuego;

    // Start is called before the first frame update
    void Start()
    {
        minijuego = null;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mh = Input.GetAxis("Horizontal");
        mv = Input.GetAxis("Vertical");

        Vector2 movePlayer = rb.position;

        playerInput = new Vector2(mh, mv);
        playerInput = Vector2.ClampMagnitude(playerInput, 1);

        rb.MovePosition(movePlayer + playerInput * speed * Time.deltaTime);

        if (minijuego != null && Input.GetKey(KeyCode.E))
        {
            minijuego.LoadScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger detectado");

        if (other.transform.tag == "minijuego")
        {
            minijuego = other.GetComponent<Minijuego>();
            minijuego.ShowCanvas();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("saliendo del trigger");
        if (other.transform.tag == "minijuego")
        {
            minijuego.HideCanvas();
            minijuego = null;
        }
    }
}
