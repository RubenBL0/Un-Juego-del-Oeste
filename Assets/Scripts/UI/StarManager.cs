using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public Star estrellaBronce, estrellaPlata, estrellaOro;
    public static StarManager instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void WinGameWithStar(Dificultad dif)
    {
        switch (dif)
        {
            case Dificultad.Facil:
                ShowStar(estrellaBronce);
                break;
            case Dificultad.Medio:
                ShowStar(estrellaPlata);
                break;
            case Dificultad.Dificil:
                ShowStar(estrellaOro);
                break;
        }
    }

    public void ShowStar(Star star)
    {
        star.StartAnimation();
    }
}
