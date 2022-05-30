using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField, Range(0f, 20f)] private float speed;

    private float mh;
    private float mv;
    
    private Vector2 playerInput;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
