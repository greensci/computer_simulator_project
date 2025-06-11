using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class computerCase : MonoBehaviour
{
  public pcOS pcOS;
  public pcOS noOS;
  public computerMonitor currentMonitor;
  public bool hasHDD1 = false;
  public bool hasHDD2 = false;
  public bool hasHDD3 = false;
  public bool hasMOBO = false;
  public bool hasPowerSupply = false;
  public bool hasCPU = false;
  public bool hasRAM1 = false;
  public bool hasRAM2 = false;
  public bool hasGPU1 = false;
  public bool hasGPU2 = false;
  public bool hasGPU3 = false;
  public bool hasCPUFan = false;
  public bool hasDVDDrive = false;

  [Header("past pc parts")]


  public bool hadHDD1 = false;
  public bool hadHDD2 = false;
  public bool hadHDD3 = false;
  public bool hadRAM1 = false;
  public bool hadRAM2 = false;
  public bool hadGPU1 = false;
  public bool hadGPU2 = false;
  public bool hadGPU3 = false;
  public bool hadCPUFan = false;
  public bool hadDVDDrive = false;
  public GameObject hdd1;
  public GameObject hdd2;
  public GameObject hdd3;
  public GameObject mobo;
  public GameObject powerSupply;
  public GameObject cpu;
  public GameObject ram1;
  public GameObject ram2;
  public GameObject gpu1;
  public GameObject gpu2;
  public GameObject gpu3;
  public GameObject cpuFan;
  public GameObject dvdDrive;
  public GameObject onLight;
  public List<GameObject> hddList;


  public LayerMask caseLayer;
  bool cpuFanChecked = false;
  [Header("pcos thing")]
  public bool isPcON = false;

  [Header("audio thing")]
  public AudioSource pcAudio;

  //other

  objectScript cpuBeforeBurning;

  void Start()
  {
    noOS.gameObject.SetActive(true);
  }


  void Update()
  {
    if (hasMOBO)
    {
      ram1 = mobo.GetComponent<moboScript>().ram1;
      ram2 = mobo.GetComponent<moboScript>().ram2;
      cpu = mobo.GetComponent<moboScript>().cpu;
      gpu1 = mobo.GetComponent<moboScript>().gpu1;
      gpu2 = mobo.GetComponent<moboScript>().gpu2;
      gpu3 = mobo.GetComponent<moboScript>().gpu3;
      cpuFan = mobo.GetComponent<moboScript>().cpuFan;

      hasCPU = mobo.GetComponent<moboScript>().hasCPU;
      hasGPU1 = mobo.GetComponent<moboScript>().hasGPU1;
      hasGPU2 = mobo.GetComponent<moboScript>().hasGPU2;
      hasGPU3 = mobo.GetComponent<moboScript>().hasGPU3;

      hasRAM1 = mobo.GetComponent<moboScript>().hasRAM1;
      hasRAM2 = mobo.GetComponent<moboScript>().hasRAM2;
      hasCPUFan = mobo.GetComponent<moboScript>().hasCPUFan;

    }
    else
    {
      ram1 = null;
      ram2 = null;
      cpu = null;
      gpu1 = null;
      gpu2 = null;
      gpu3 = null;
      cpuFan = null;

      hasCPU = false;
      hasGPU1 = false;
      hasGPU2 = false;
      hasGPU3 = false;
      hasRAM1 = false;
      hasRAM2 = false;
      hasCPUFan = false;
    }

    if (isPcON)
    {

      if (hasMOBO && hasPowerSupply && hasCPU)
      {

        if (cpu.GetComponent<objectScript>().isObjDamaged == true)
        {
          //cpu is damaged
          StartCoroutine(main.setErrorMessage("CPU is burned!"));
          isPcON = false;

        }
        else
          onLight.SetActive(true);

        if (hadRAM1)
        {
          if (!hasRAM1)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadRAM1)
        {
          if (hasRAM1)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadRAM2)
        {
          if (!hasRAM2)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadRAM2)
        {
          if (hasRAM2)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadGPU1)
        {
          if (!hasGPU1)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadGPU1)
        {
          if (hasGPU1)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadGPU2)
        {
          if (!hasGPU2)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadGPU2)
        {
          if (hasGPU2)
          {
            pcOS.onBSOD = true;
          }
        }
        if (hadGPU3)
        {
          if (!hasGPU3)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadGPU3)
        {
          if (hasGPU3)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadHDD1)
        {
          if (!hasHDD1)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadHDD1)
        {
          if (hasHDD1)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadHDD2)
        {
          if (!hasHDD2)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadHDD2)
        {
          if (hasHDD2)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadHDD3)
        {
          if (!hasHDD3)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadHDD3)
        {
          if (hasHDD3)
          {
            pcOS.onBSOD = true;
          }
        }

        if (hadDVDDrive)
        {
          if (!hasDVDDrive)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hadDVDDrive)
        {
          if (hasDVDDrive)
          {
            pcOS.onBSOD = true;
          }
        }
        if (!hasCPUFan)
        {
          if (!cpuFanChecked)
          {
            if (cpu.GetComponent<objectScript>().isObjDamaged == false)
            {
              cpuBeforeBurning = cpu.GetComponent<objectScript>();
              Invoke("burnCpu", UnityEngine.Random.Range(3, 10));
              cpuFanChecked = true;
            }
          }

        }
        else
        {
          cpuFanChecked = false;
        }

      }
      else
      {
        isPcON = false;
      }
    }
    else
    {
      /* ignore this.
      if (createID)
      {
      
        string thinger = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for (int i = 0; i < 4; i++)
        {
          sessionID += thinger[UnityEngine.Random.Range(0, thinger.Length)];
        }
        createID = false;
      }*/

      onLight.SetActive(false);
      hadDVDDrive = hasDVDDrive;
      hadGPU1 = hasGPU1;
      hadGPU2 = hasGPU2;
      hadGPU3 = hasGPU3;
      hadHDD1 = hasHDD1;
      hadHDD2 = hasHDD2;
      hadHDD3 = hasHDD3;
      hadRAM1 = hasRAM1;
      hadRAM2 = hasRAM2;
      if (mobo == null)
      {
        hasMOBO = false;
      }
      if (powerSupply == null)
      {
        hasPowerSupply = false;
      }
      if (gpu1 == null)
      {
        hasGPU1 = false;
      }
      if (gpu2 == null)
      {
        hasGPU2 = false;
      }
      if (gpu3 == null)
      {
        hasGPU3 = false;
      }
      if (hdd1 == null)
      {
        hasHDD1 = false;
      }
      if (hdd2 == null)
      {
        hasHDD2 = false;
      }
      if (hdd3 == null)
      {
        hasHDD3 = false;
      }
      if (ram1 == null)
      {
        hasRAM1 = false;
      }
      if (ram2 == null)
      {
        hasRAM2 = false;
      }


      if (hddList.Count > 0)
      {

        pcOS = hddList[0].GetComponentInChildren<pcOS>();
        pcOS.hardDrive.curcase = this;
        pcOS.hardDrive.canvas.worldCamera = currentMonitor.monitorCam;

        noOS.hardDrive.canvas.worldCamera = null;


      }
      else
      {

        if (pcOS != null)
        {
          pcOS.hardDrive.canvas.worldCamera = null;
        }






        pcOS = noOS;
      }
      /*if (dvdDrive.GetComponent<DiskDrive>().hasDisc)
      {
       
      }*/
      if (hasRAM1 || hasRAM2)
      {

      }
      else
      {
        pcOS.hardDrive.canvas.worldCamera = null;
      }




    }
  }
  public void burnCpu()
  {
    if (isPcON)
    {
      if (cpuBeforeBurning == cpu.GetComponent<objectScript>())
        cpu.GetComponent<objectScript>().isObjDamaged = true;
    }
  }
  public void shutdownPc()
  {
    if (pcOS.booted && !pcOS.onBSOD && pcOS.operativeSystemType != pcOS.OperativeSystem.No_Boot_Device)
    {
      pcOS.shuttingDownBool = true;
      Invoke("thinger", 5f);
    }
    else
    {
      Invoke("thinger", 0.5f);
    }


  }
  void thinger()
  {
    isPcON = false;

    pcOS.shuttingDownBool = false;
  }
}