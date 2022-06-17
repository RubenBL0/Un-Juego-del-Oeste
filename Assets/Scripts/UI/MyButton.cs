using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color colorHover;
    [SerializeField] Color colorExit;

    TextMeshProUGUI txt;
    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = colorExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TextMeshProUGUI>();
        colorExit = txt.color;
    }
}
