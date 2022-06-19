using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class BotonDificultad : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool mouse_over = false;
    [SerializeField] Dificultad dificultad = Dificultad.Facil;
    [SerializeField] Minijuego minijuego;

    [SerializeField] Color colorHover;
    [SerializeField] Color colorExit;
    [SerializeField] Color colorLocked;

    [SerializeField] TextMeshProUGUI txtDificultad;

    public bool enabled = false;

    private void Start()
    {
        txtDificultad.text = dificultad.ToString();
    }

    void Update()
    {
        if (enabled)
        {
            GetComponent<Button>().enabled = true;
            txtDificultad.color = colorExit;
        }
        if (mouse_over && enabled)
        {
            txtDificultad.color = colorHover;
        }
        if (!enabled)
        {
            GetComponent<Button>().enabled = false;
            txtDificultad.color = colorLocked;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        txtDificultad.color = colorExit;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mouse_over = false;
        txtDificultad.color = colorExit;
    }
}

public enum Dificultad
{
    Facil, Medio, Dificil
}
