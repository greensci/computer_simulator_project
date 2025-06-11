using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class aboutPC_Main : MonoBehaviour
{
    public GameObject mainObj;
    public TMP_Text infoText;

    public bool windowOpened = false;
    public GameObject focusedParent;
    public GameObject unfocusedParent;
    public GameObject minimizedParent;
    public pcOS pcOs;



    void Start()
    {
        mainObj.SetActive(false);
    }
    void Update()
    {
        //infoText.text = "This is a PC. It is a computer. It has a motherboard, a CPU, a GPU, RAM, and a CPU fan. It also has a power supply, a hard drive, and a case. It has a monitor, a keyboard, and a mouse. It runs an operating system. It can run apps. It can browse the internet. It can play games. It can do many things. It is a PC.";

        string cpuname = "None. (Are you a wizard?)";
        string gpuname1 = "None.";
        string gpuname2 = "None.";
        string gpuname3 = "None.";
        string osInfo = "No OS Installed. (How are you reading this?)";
        int ramamount = 0;
        int vramamount = 0;

        int storage = 0;
        computerCase computer = pcOs.hardDrive.curcase;
        if (computer != null)
        {
            osInfo = "Greensoft ® Tinglows\n";
            osInfo += pcOs.operativeSystemType.ToString().Replace("_", " ") + " " + pcOs.operativeSystemEdition.ToString().Replace("_", " ")+ "\n";
            osInfo += "Confidential Beta Build. DO NOT REDISTRIBUTE!\n";
            osInfo += "Copyright © 2001 Greensoft Corporation\n\nSpecs:\n\n";
            


            if (computer.cpu != null)
            {
                cpuname = computer.cpu.GetComponent<objectScript>().cpuBrand.ToString() + "(R) " + computer.cpu.GetComponent<objectScript>().name + " " + computer.cpu.GetComponent<objectScript>().other;
                if (computer.cpu.GetComponent<objectScript>().isx64)
                    cpuname += " x64";
                else
                    cpuname += " x86";
            }
            if (computer.gpu1 != null)
            {
                gpuname1 = computer.gpu1.GetComponent<objectScript>().gpuBrand.ToString() + "(R) " + computer.gpu1.GetComponent<objectScript>().name + " " + computer.gpu1.GetComponent<objectScript>().objSpeed + " MHz";
                vramamount += computer.gpu1.GetComponent<objectScript>().value;
            }
            else
            {
                if (computer.mobo.GetComponent<objectScript>().hasiGPU)
                    gpuname1 = "Integrated " + computer.mobo.GetComponent<objectScript>().moboChipset.Replace("MVIDIA", "MVIDIA(R)").Replace("Entel", "Entel(R)").Replace("CiS", "CiS(R)");
                else
                    gpuname1 = "None";
            }
            if (computer.gpu2 != null)
            {
                gpuname2 = computer.gpu2.GetComponent<objectScript>().gpuBrand.ToString() + "(R) " + computer.gpu2.GetComponent<objectScript>().name + " " + computer.gpu2.GetComponent<objectScript>().objSpeed + " MHz";
                vramamount += computer.gpu2.GetComponent<objectScript>().value;
            }
            if (computer.gpu3 != null)
            {
                gpuname3 = computer.gpu3.GetComponent<objectScript>().gpuBrand.ToString() + "(R) " + computer.gpu3.GetComponent<objectScript>().name + " " + computer.gpu3.GetComponent<objectScript>().objSpeed + " MHz";
                vramamount += computer.gpu3.GetComponent<objectScript>().value;
            }

            if (computer.ram1 != null)
            {
                ramamount += computer.ram1.GetComponent<objectScript>().value;
            }
            if (computer.ram2 != null)
            {
                ramamount += computer.ram2.GetComponent<objectScript>().value;
            }

            if (computer.hdd1 != null)
            {
                storage += computer.hdd1.GetComponent<objectScript>().value;
            }
            if (computer.hdd2 != null)
            {
                storage += computer.hdd2.GetComponent<objectScript>().value;
            }
            if (computer.hdd3 != null)
            {
                storage += computer.hdd3.GetComponent<objectScript>().value;
            }


            infoText.text = osInfo + "CPU: " + cpuname + "\nGPU(s): \n" + gpuname1 + "\n" + gpuname2 + "\n" + gpuname3 + "\nRAM: " + ramamount + "MB\nVRAM: " + vramamount + "MB\nStorage: " + storage + "GB";

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
    }
    public void CloseApp()
    {
        windowOpened = false;
        this.gameObject.transform.parent = unfocusedParent.transform;
        mainObj.transform.parent = unfocusedParent.transform;
    } public void MinimizeApp()
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

}