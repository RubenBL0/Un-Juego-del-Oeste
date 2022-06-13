using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField, Range(0f, 100f)] private float speed;

    private float mh;
    private float mv;
    
    private Vector2 playerInput;

    private MinijuegoTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        print(GameManager.instance.playerTransform);
        if(GameManager.instance.playerTransform != null)
        {
            print(GameManager.instance.playerTransform.position);
            SetPosition(GameManager.instance.playerTransform);
        }
        trigger = null;
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

        if (trigger != null && Input.GetKey(KeyCode.E))
        {
            trigger.InfoCanvas();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger detectado");

        if (other.transform.tag == "minijuego")
        {
            trigger = other.GetComponent<MinijuegoTrigger>();
            trigger.ShowCanvas();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("saliendo del trigger");
        if (other.transform.tag == "minijuego")
        {
            trigger.HideCanvas();
            trigger = null;
        }
    }

    public void SetPosition(Transform transf)
    {
        rb.isKinematic = true;
        transform.position = transf.position;
        transform.rotation = transf.rotation;
        rb.isKinematic = false;
    }
}
