using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Vaquillas vaquilla;
    [SerializeField] private float speed;
    private Vector3 dragOffset;
    private Camera _cam;
    

    private void Awake()
    {
        _cam = Camera.main;
    }
    private void OnMouseDown()
    {
        vaquilla.Atrapada();
        vaquilla.linea.DibujaLinea();
        dragOffset = transform.position - GetMousePos();
    }
    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }
    private void OnMouseUp()
    {
        vaquilla.Liberada();
    }
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
