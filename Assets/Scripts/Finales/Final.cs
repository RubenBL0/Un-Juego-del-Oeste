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
        StartCoroutine(Textos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Textos()
    {
        foreach(TextoFinales txt in listaTextos)
        {
            yield return new WaitForSeconds(txt.tiempoEmpezar);
            foreach (char c in txt.contenido)
            {
                txt.texto.text = txt.texto.text + c;
                yield return new WaitForSeconds(txt.tiempoLetra);
            }
        }
        yield return new WaitForSeconds(7f);
        GameManager.instance.ReturnToMainMenu();
    }
}
