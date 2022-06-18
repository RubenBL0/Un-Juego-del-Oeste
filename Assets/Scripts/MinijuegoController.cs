using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DifficultyEasy()
    {

    }
    public virtual void DifficultyMedium()
    {

    }
    public virtual void DifficultyHard()
    {

    }

    public virtual void Play()
    {
        AudioSource audio = GetComponent<AudioSource>();
        GameManager.instance.StopMusic();
        audio.Play();
        audio.volume = DataManager.instance.Volume();
    }
}
