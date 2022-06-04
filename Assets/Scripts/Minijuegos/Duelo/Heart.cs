using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] float beatTime = 1f;
    [SerializeField] GameObject fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmptyHeart()
    {
        fill.SetActive(false);
    }

    IEnumerator HeartCoroutine()
    {
        while (true) //Cambiar por otra cosa
        {
            HeartBeat();
            yield return new WaitForSeconds(beatTime);
        }
        yield return null;
    }

    void HeartBeat()
    {
        transform.localScale *= 2f;
    }

    public void SpeedUpBeat()
    {
        beatTime *= 0.5f;
    }
}
