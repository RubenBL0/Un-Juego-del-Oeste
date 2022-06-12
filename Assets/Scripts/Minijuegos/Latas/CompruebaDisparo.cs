using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompruebaDisparo : MonoBehaviour
{
    public static event Action comprobarDisparo;
    private void OnMouseDown()
    {
        comprobarDisparo?.Invoke();
    }
}
