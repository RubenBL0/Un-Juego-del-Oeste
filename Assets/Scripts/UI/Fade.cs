using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private GameObject sceneLoadManager;

    void DesactivaGo()
    {
        sceneLoadManager.SetActive(false);
    }
}
