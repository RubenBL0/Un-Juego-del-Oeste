using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    public LineRenderer lr;
    public Transform[] lr_points = new Transform[2];

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    /*public void DibujaLinea(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.lr_points = points;
    }*/
    public void DibujaLinea()
    {
        lr.positionCount = lr_points.Length;
    }
    private void Update()
    {
        if (lr.enabled)
        {
            for (int i = 0; i < lr_points.Length; i++)
            {
                lr.SetPosition(i, lr_points[i].position);
            }
        }        
    }
}
