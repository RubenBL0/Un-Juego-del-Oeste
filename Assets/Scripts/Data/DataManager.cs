using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int slot;
    public static DataManager instance;
    public int volume;
    public int sfx;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        volume = GetVolume();
        sfx = GetSFX();
    }

    private void Start()
    {
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }

    public void SavePosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("posX" + slot, position.x);
        PlayerPrefs.SetFloat("posY" + slot, position.y);
    }

    public Vector3 GetPosition()
    {
        Vector3 loadedPosition = Vector3.zero;
        if(PlayerPrefs.HasKey("posX" + slot))
        {
            loadedPosition.x = PlayerPrefs.GetFloat("posX" + slot);
            loadedPosition.y = PlayerPrefs.GetFloat("posY" + slot);
        }

        return loadedPosition;
    }

    public void SaveMinigameScore(MinigameNames minigame, int score)
    {
        print(minigame.ToString() + slot);
        PlayerPrefs.SetInt(minigame.ToString() + slot, score);
    }

    public MinigameScore GetMinigameScore(MinigameNames minigame)
    {
        int score = PlayerPrefs.GetInt(minigame.ToString() + slot, 0);

        switch (score)
        {
            case 1:
                return MinigameScore.Bronze;
            case 2:
                return MinigameScore.Silver;
            case 3:
                return MinigameScore.Gold;
            default:
                return MinigameScore.None;
        }
    }



    public void SaveData()
    {
        PlayerPrefs.Save();
    }

    public void DeleteSlot(int slot)
    {
        PlayerPrefs.DeleteKey("posX" + slot);
        PlayerPrefs.DeleteKey("posY" + slot);
        //Borrar minijuegos del archivo de guardado
        PlayerPrefs.DeleteKey(MinigameNames.Vacas.ToString() + slot);
        PlayerPrefs.DeleteKey(MinigameNames.Duelo.ToString() + slot);
        PlayerPrefs.DeleteKey(MinigameNames.Latas.ToString() + slot);
        PlayerPrefs.DeleteKey(MinigameNames.Pelea.ToString() + slot);
        PlayerPrefs.DeleteKey(MinigameNames.Escupitajo.ToString() + slot);
        PlayerPrefs.DeleteKey(MinigameNames.Tren.ToString() + slot);
    }

    public int GetVolume()
    {
        return PlayerPrefs.GetInt("volume", 50);
    }

    public void SetVolume(int vol)
    {
        vol = vol < 0 ? 0 : vol;
        vol = vol > 100 ? 100 : vol;
        PlayerPrefs.SetInt("volume", vol);
        volume = vol;
    }

    public int GetSFX()
    {
        return PlayerPrefs.GetInt("sfx", 50);
    }

    public void SetSFX(int sfx)
    {
        sfx = sfx < 0 ? 0 : sfx;
        sfx = sfx > 100 ? 100 : sfx;
        PlayerPrefs.SetInt("sfx", sfx);
        this.sfx = sfx;
    }


    //Usar este método para obtener el volumen como fracción entre 0 y 1
    public float Volume()
    {
        return volume / 100f;
    }

    public float SFX()
    {
        return sfx / 100f;
    }
}
