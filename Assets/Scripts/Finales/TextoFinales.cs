using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoFinales : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public string contenido;
    [Range(0.01f, 1f)]public float tiempoLetra =.075f;
    public float tiempoEmpezar = 3f;

    private void Awake()
    {
        texto = GetComponent<TextMeshProUGUI>();
        contenido = texto.text;
        texto.text = "";
    }
}
