using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTaburete : MonoBehaviour
{
    public Transform salidaDisparo;
    public Transform mira;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ReconocerObxecto();
        }
    }

    public void ReconocerObxecto()
    {
        RaycastHit2D hit = Physics2D.Raycast(salidaDisparo.position, (mira.position - salidaDisparo.position).normalized, 100f);

        if (hit.collider.gameObject.CompareTag("taburete"))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            hit.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

    }
}
