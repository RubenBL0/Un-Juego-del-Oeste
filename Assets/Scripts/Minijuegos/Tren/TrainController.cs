using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
}
