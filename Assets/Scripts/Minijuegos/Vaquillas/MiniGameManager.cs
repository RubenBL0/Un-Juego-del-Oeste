using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_puntos;
    [SerializeField] private GameObject vaquilla;
    [SerializeField] private Collider2D suelo;
    [SerializeField] private int n_vacas;
    private int puntos;
    private int puntosAnteriores;    

    private void OnEnable()
    {
        Vaquillas.establoEntrando += SumaVacas;
        Vaquillas.establoSaliendo += RestaVacas;
    }
    private void OnDisable()
    {
        Vaquillas.establoEntrando -= SumaVacas;
        Vaquillas.establoSaliendo -= RestaVacas;
    }
    void Start()
    {
        puntos = 0;
        puntosAnteriores = 0;
        StartCoroutine(SumaPuntos());
    }
    void Update()
    {
        t_puntos.text = puntos.ToString();

        if (puntos > puntosAnteriores + 20)
        {
            Instantiate(vaquilla, GetPosSpawn(), Quaternion.identity);
            ActualizaPuntosAnteriores();
        }
    }
    Vector2 GetPosSpawn()
    {
        Vector2 vaquillaPos = Vector2.zero;
        Bounds bounds = suelo.bounds;
        vaquillaPos.x = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
        vaquillaPos.y = Random.Range(bounds.min.y + 1, bounds.max.y - 1);
        return vaquillaPos;
    }
    void ActualizaPuntosAnteriores()
    {
        puntosAnteriores = puntos;
    }
    void SumaVacas()
    {
        n_vacas++;
    }
    void RestaVacas()
    {
        n_vacas--;
    }
    IEnumerator SumaPuntos()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            puntos += 1 * n_vacas;
        }
    }
}
