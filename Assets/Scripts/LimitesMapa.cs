using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitesMapa : MonoBehaviour
{
    public delegate void Del_Limites(PolygonCollider2D collider);
    public static event Del_Limites recogeLimites;

    void Start()
    {
        recogeLimites?.Invoke(gameObject.GetComponent<PolygonCollider2D>());
    }
}
