using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource currentSong;
    public static void PlaySound(AudioSource audio)
    {
        audio.Play();
        audio.volume = DataManager.instance.SFX();
    }

    public static void PlayMusic(AudioSource audio)
    {
        if(currentSong != null)
        {
            currentSong.Stop();
        }
        audio.Play();
        audio.volume = DataManager.instance.Volume();
        currentSong = audio;
    }

    public static void StopMusic()
    {
        if(currentSong != null)
        {
            currentSong.Stop();
        }
        currentSong = null;
    }
}
