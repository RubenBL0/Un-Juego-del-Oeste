using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalesManager : MonoBehaviour
{
    public Final[] finales = new Final[4];

    private void Start()
    {
        int final = GameManager.instance.final;
        ShowFinal(final);
    }

    public void ShowFinal(int final)
    {
        finales[final].gameObject.SetActive(true);
    }
}
