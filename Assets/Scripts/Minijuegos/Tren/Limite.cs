using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] obstaculos = new GameObject[3];
    private float distancia;
    private void Awake()
    {
        distancia = player.transform.position.x - transform.position.x;
    }

    void Update()
    {        
        transform.position = new Vector2(player.transform.position.x - distancia, -3.19f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "obstaculo")
        {
            float distObstaculo = Random.Range(15f, 30f);

            if (obstaculos[0] == null)
            {
                obstaculos[0] = collision.transform.gameObject;
            }
            else if (obstaculos[1] == null)
            {
                obstaculos[1] = collision.transform.gameObject;
            }
            else if (obstaculos[2] == null)
            {
                obstaculos[2] = collision.transform.gameObject;
            }
            else
            {
                obstaculos[0].transform.position = new Vector2(player.transform.position.x + distObstaculo, obstaculos[0].transform.position.y);
                obstaculos[0] = obstaculos[1];
                obstaculos[1] = obstaculos[2];
                obstaculos[2] = collision.transform.gameObject;

            }

            //collision.transform.position = new Vector2(player.transform.position.x + distObstaculo, collision.transform.position.y);
        }
    }
}
