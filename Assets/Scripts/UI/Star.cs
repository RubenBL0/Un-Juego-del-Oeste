using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public void StartAnimation()
    {
        Time.timeScale = 0f;
        GetComponent<Animator>().SetTrigger("MovimientoEstrella");
    }

    public void StopAnim()
    {
        Time.timeScale = 1f;
        GameManager.instance.VictoriaStarOut();
        GameManager.instance.LoadOverworld();
        GetComponent<Animator>().SetTrigger("Remove");
    }
}
