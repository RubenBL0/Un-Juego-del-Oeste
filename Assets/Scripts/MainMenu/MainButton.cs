using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    public void OnClickPlay()
    {
        MainMenuManager.instance.ShowFilesCanvas();
    }

    public void OnClickOptions()
    {
        MainMenuManager.instance.ShowOptionsCanvas();
    }

    public void OnClickExit()
    {
        //GUARDAR?
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnClickBack()
    {
        MainMenuManager.instance.ShowMainCanvas();
    }

    public void OnClickFile(int slot)
    {
        DataManager.instance.SetSlot(slot);
        SceneManager.LoadScene("MapaPueblo");
    }

    public void OnClickDelete(int slot)
    {
        DataManager.instance.DeleteSlot(slot);
    }
}
