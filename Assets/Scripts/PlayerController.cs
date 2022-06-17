using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    [SerializeField, Range(1f, 10f)] private int speed;

    private float mh;
    private float mv;
    
    private Vector2 playerInput;


    private MinijuegoTrigger trigger;

    private Animator playerAnimator;

    private Minijuego minijuego;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trigger = null;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mh = Input.GetAxisRaw("Horizontal");
        mv = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("Horizontal", mh);
        playerAnimator.SetFloat("Vertical", mv);
        playerAnimator.SetFloat("Speed", playerInput.sqrMagnitude);

        if (trigger != null && Input.GetKey(KeyCode.E))
        {
            trigger.InfoCanvas();
        }
    }
    private void FixedUpdate()
    {
        MuevePlayer();
    }

    private void MuevePlayer()
    {
        Vector2 movePlayer = rb.position;

        playerInput = new Vector2(mh, mv).normalized;

        rb.MovePosition(movePlayer + playerInput * speed * Time.fixedDeltaTime);
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
            if (trigger != null)
            {
                trigger.HideCanvas();
                trigger = null;
            }
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
