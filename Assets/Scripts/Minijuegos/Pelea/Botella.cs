using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botella : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemigo")
        {
            collision.transform.GetComponent<Enemigo>().Muerte();
            Destroy(gameObject);
        }
        if (collision != null)
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
