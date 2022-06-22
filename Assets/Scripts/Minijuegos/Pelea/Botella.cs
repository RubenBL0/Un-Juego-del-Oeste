using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botella : MonoBehaviour
{
    [SerializeField] private AudioSource botellaRota;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemigo")
        {
            collision.transform.GetComponent<Enemigo>().Muerte();
            SoundManager.PlaySound(botellaRota);
            Invoke("DestruirBotella", 1.5f);
        }
        if (collision != null)
        {
            Debug.Log(collision.gameObject.name);
            SoundManager.PlaySound(botellaRota);
            Invoke("DestruirBotella", 1.5f);
        }
    }
    void DestruirBotella()
    {
        Destroy(gameObject);
    }
}
