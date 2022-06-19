using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

    public Canvas mainCanvas;
    public Canvas filesCanvas;
    public Canvas optionCanvas;

    public Slider volumeSlider;
    public TextMeshProUGUI txtVolume;
    public Slider sfxSlider;
    public TextMeshProUGUI txtSFX;

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
        txtVolume.text = volumeSlider.value.ToString();
        sfxSlider.value = DataManager.instance.SFX() * 100;
        txtSFX.text = sfxSlider.value.ToString();
        print(sfxSlider.value);

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
        txtVolume.text = volumeSlider.value.ToString();
        DataManager.instance.SetVolume(volume);
        foreach(AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.volume = DataManager.instance.Volume();
        }
    }

    public void OnChangeSFX()
    {
        int sfx = (int)sfxSlider.value;
        txtSFX.text = sfxSlider.value.ToString();
        DataManager.instance.SetSFX(sfx);
    }
}
