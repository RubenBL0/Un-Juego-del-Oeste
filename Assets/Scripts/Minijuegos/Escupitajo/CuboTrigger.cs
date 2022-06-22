using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource cubo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            SoundManager.PlaySound(cubo);
        }
    }
}
