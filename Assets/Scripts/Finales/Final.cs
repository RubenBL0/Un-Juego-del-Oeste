using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Final : MonoBehaviour
{
    public Image imageFondo;
    public List<TextoFinales> listaTextos = new List<TextoFinales>();

    // Start is called before the first frame update
    void Start()
    {
        imageFondo.color = Color.black;
        StartCoroutine(Textos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Textos()
    {
        int texto = 0;
        bool changed = false;
        foreach(TextoFinales txt in listaTextos)
        {
            texto++;
            yield return new WaitForSeconds(txt.tiempoEmpezar);
            foreach (char c in txt.contenido)
            {
                if(texto == 3 && !changed)
                {
                    changed = true;
                    StartCoroutine(Gradient());
                }
                txt.texto.text = txt.texto.text + c;
                yield return new WaitForSeconds(txt.tiempoLetra);
            }
        }
        yield return new WaitForSeconds(7f);
        GameManager.instance.ReturnToMainMenu();
    }

    IEnumerator Gradient()
    {
        float t = 0f;
        while (t < 1f)
        {
            Color value = Color.Lerp(Color.black, Color.white, t);
            t += Time.deltaTime;
            imageFondo.color = value;
            yield return null;
        }
        yield return null;
    }
}
