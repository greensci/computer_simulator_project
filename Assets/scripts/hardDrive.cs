using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

public class hardDrive : MonoBehaviour
{
    public string hdName = "";
    public string hddLetter = ""; // the thing that says the hdd thing like C:, D:, F:, etc
    public int hdSize = 0;
    public bool isBootable = false;
    public bool hasOS = false;
    public GameObject bootSection;
    public GameObject storageSection;
    public computerCase curcase;
    public GameObject noBootableSystemError;
    public Canvas canvas;
    public Camera displayCam;
    public RenderTexture displayTex;
    void Start()
    {

        if (hdName == "")
        {
            string charTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            hdName = hdSize + "GB";
            for (int i = 0; i < 10; i++)
            {
                hdName += charTable[Random.Range(0, charTable.Length)];
            }

        }
        if (!Application.isEditor)
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/saveFiles"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/saveFiles");
            }

            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/saveFiles/" + main.levelName);

            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/saveFiles/" + main.levelName + "/" + hdName))
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/saveFiles/" + main.levelName + "/" + hdName);

        }

    }
    void Update()
    {
        curcase = GetComponentInParent<computerCase>();
    }

}