using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField, Range(0f, 20f)] private float speed;

    private float mh;
    private float mv;

    private bool activable;
    
    private Vector2 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        activable = false;
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

        if (activable && Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("PlaceHolder_Minijuego");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger detectado");

        if (other.transform.tag == "minijuego")
        {
            activable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("saliendo del trigger");
        if (other.transform.tag == "minijuego")
        {
            activable = false;
        }
    }
}
