using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompruebaDisparo : MonoBehaviour
{
    public static event Action comprobarDisparo;
    [SerializeField] private AudioSource disparo;

    private void OnMouseDown()
    {
        SoundManager.PlaySound(disparo);
        comprobarDisparo?.Invoke();
    }
}
