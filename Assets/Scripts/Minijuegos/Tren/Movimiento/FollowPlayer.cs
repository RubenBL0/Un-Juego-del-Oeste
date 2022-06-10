using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float distancia;

    private void Awake()
    {
        distancia = Vector2.Distance(transform.position, player.transform.position);
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + distancia, transform.position.y, transform.position.z);
    }
}
