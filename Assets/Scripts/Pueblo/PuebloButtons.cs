using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuebloButtons : MonoBehaviour
{
    public void OnClickResume()
    {
        GameManager.instance.ResumeGame();
    }

    public void OnClickExit()
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.ReturnToMainMenu();
    }

    public void OnCickLeaveMinigame()
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.LoadOverworld();
    }

    public void OnClickOptions()
    {
        GameManager.instance.ShowOptionsMenu();
    }

    public void OnClickBackOptions()
    {
        GameManager.instance.HideOptionsMenu();
    }
}
