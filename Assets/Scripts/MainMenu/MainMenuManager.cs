using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

    public Canvas mainCanvas;
    public Canvas filesCanvas;
    public Canvas optionCanvas;

    public Slider volumeSlider;

    AudioSource audio;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = DataManager.instance.Volume() * 100;
        audio = GetComponent<AudioSource>();
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        if (PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }
        PlayMusic();
        ShowMainCanvas();
    }

    void PlayMusic()
    {
        audio.volume = DataManager.instance.Volume();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainCanvas()
    {
        filesCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
    }

    public void ShowOptionsCanvas()
    {
        filesCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    public void ShowFilesCanvas()
    {
        optionCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        filesCanvas.gameObject.SetActive(true);
    }

    public void OnChangeVolume()
    {
        int volume = (int)volumeSlider.value;
        DataManager.instance.SetVolume(volume);
        foreach(AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.volume = DataManager.instance.Volume();
        }
    }
}
