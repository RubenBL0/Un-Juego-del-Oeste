using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPoint2 : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }
    void Update()
    {
        transform.position = GetMousePos();
    }
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
