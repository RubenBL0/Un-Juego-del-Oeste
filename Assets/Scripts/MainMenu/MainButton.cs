using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color colorHover;
    [SerializeField] Color colorExit;

    TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TextMeshProUGUI>();
        colorExit = txt.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = colorExit;
    }

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
