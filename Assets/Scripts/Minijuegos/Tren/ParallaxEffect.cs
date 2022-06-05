using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;

    private Transform cameraTransform;
    private Vector3 prevCamPosition;
    private float spriteWidth, startPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        prevCamPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x; //devuelve el ancho del sprite
        startPosition = transform.position.x;
    }
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - prevCamPosition.x) * parallaxMultiplier; //devuelve cuánto se movió la cámara en x desde el frame anterior
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultiplier); //devuelve cuánto se movió la cámara respecto a la capa
        transform.Translate(new Vector3(deltaX, 0, 0));
        prevCamPosition = cameraTransform.position;

        //reposiciona las capas si su desplazamiento es mayor a su posición inicial + su ancho
        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth; 
        }
    }
}
