using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Datos Enemigo", menuName = "Datos Enemigo")]
public class DatosEnemigo : ScriptableObject
{
    [SerializeField] private int vida;
    [SerializeField] private float speed;
    [SerializeField] private int dano;
    [SerializeField] private float frecuenciaAtaque;

    public int Vida { get { return vida; }}
    public int Dano { get { return dano; }}
    public float FrecuenciaAtaque { get {return frecuenciaAtaque; }}
    public float Speed { get { return speed; }}
}
