using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogeLimites : MonoBehaviour
{
    private CinemachineConfiner2D cinemachineConfiner;

    private void OnEnable()
    {
        LimitesMapa.recogeLimites += SetCollider;
    }
    private void OnDisable()
    {
        LimitesMapa.recogeLimites -= SetCollider;
    }

    private void Awake()
    {
        cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
    }
    void SetCollider(PolygonCollider2D collider)
    {
        cinemachineConfiner.m_BoundingShape2D = collider;
    }
}
