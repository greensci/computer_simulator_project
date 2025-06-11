using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class personalization_Main : MonoBehaviour
{
    public GameObject mainObj;

    public bool windowOpened = false;

    public GameObject focusedParent;
    public GameObject unfocusedParent;
    public GameObject minimizedParent;
    public osDesktop desktopScript;

    void Start()
    {
        mainObj.SetActive(false);
    }
    void Update()
    {



        if (windowOpened)
        {

            mainObj.SetActive(true);

        }
        else
        {
            mainObj.SetActive(false);
        }


        if (gameObject.GetComponentInParent<pcOS>().hardDrive.curcase != null)
        {
            if (!gameObject.GetComponentInParent<pcOS>().hardDrive.curcase.isPcON)
            {
                windowOpened = false;

            }
        }
        else
        {
            windowOpened = false;
        }
    }
    public void CloseApp()
    {
        windowOpened = false;
        this.gameObject.transform.parent = unfocusedParent.transform;
        mainObj.transform.parent = unfocusedParent.transform;
    }
    public void MinimizeApp()
    {

        this.gameObject.transform.parent = minimizedParent.transform;
        mainObj.transform.parent = minimizedParent.transform;
    }
    public void OpenApp()
    {

        if (windowOpened == false)
        {
            appMain[] focusedWindows = focusedParent.GetComponentsInChildren<appMain>();
            for (int i = 0; i < focusedWindows.Length; i++)
            {
                focusedWindows[i].gameObject.transform.parent = unfocusedParent.transform;
            }

            this.gameObject.transform.parent = focusedParent.transform;
            mainObj.transform.parent = focusedParent.transform;
        }
        windowOpened = true;
    }
    public void SetWallpaper(Sprite sprite){
        desktopScript.currentWallpaper = sprite;
    }

}