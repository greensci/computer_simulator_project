using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class internet_explorer_Main : MonoBehaviour
{
    public GameObject mainObj;
    public GameObject homePageObj;
    public bool browserOpened = false;

    public bool homePageOpened = false;
    public List<GameObject> historyBack = new List<GameObject>();
    public List<GameObject> historyForward = new List<GameObject>();
    public string currentURL = "https://www.coogle.com";

    public TMP_InputField urlInput;
    public GameObject focusedParent;
    public GameObject unfocusedParent;
    public GameObject minimizedParent;

    void Start()
    {
        mainObj.SetActive(false);
        openHome();
    }
    void Update()
    {
        currentURL = urlInput.text;

        if (browserOpened)
        {

            mainObj.SetActive(true);

        }
        else
        {
            mainObj.SetActive(false);
        }
        if (homePageOpened)
        {
            urlInput.text = "https://www.coogle.com";
            homePageObj.SetActive(true);

        }
        else
        {
            homePageObj.SetActive(false);
        }

        if (gameObject.GetComponentInParent<pcOS>().hardDrive.curcase != null)
        {
            if (!gameObject.GetComponentInParent<pcOS>().hardDrive.curcase.isPcON)
            {
                browserOpened = false;
                homePageOpened = true;
            }
        }
        else
        {
            browserOpened = false;
        }
    }
    public void CloseApp()
    {
        browserOpened = false;
        homePageOpened = true;
        this.gameObject.transform.parent = unfocusedParent.transform;
        mainObj.transform.parent = unfocusedParent.transform;
    }
    public void openHome()
    {
        homePageOpened = true;
    }
    public void closeHome()
    {
        homePageOpened = false;
    }
    public void MinimizeApp()
    {
        this.gameObject.transform.parent = minimizedParent.transform;
        mainObj.transform.parent = minimizedParent.transform;
    }
    public void OpenApp()
    {


        if (browserOpened == false)
        {
            appMain[] focusedWindows = focusedParent.GetComponentsInChildren<appMain>();
            for (int i = 0; i < focusedWindows.Length; i++)
            {
                focusedWindows[i].gameObject.transform.parent = unfocusedParent.transform;
            }

            this.gameObject.transform.parent = focusedParent.transform;
            mainObj.transform.parent = focusedParent.transform;
        }
        browserOpened = true;
        homePageOpened = true;
    }

}