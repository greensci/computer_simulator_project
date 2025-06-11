using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
public class pcOS : MonoBehaviour
{
    public OperativeSystem operativeSystemType;
    public enum OperativeSystem { No_Boot_Device, None, Tinglows_XP, Tinglows_2000 };
    public OperativeSystemEdition operativeSystemEdition;
    public enum OperativeSystemEdition { Professional, Home };

    
public hardDrive hardDrive;
    public GameObject off;


    public GameObject black;
    public GameObject exitBtn;
    public GameObject BSOD;
    public GameObject blackBars;
    public GameObject operativeSystem;

    public GameObject loadingOS;
    public GameObject loadingSession;
    public GameObject shuttingDown;




    public bool onBSOD = false;

    public bool shuttingDownBool = false;



    public bool startSequence = true;
    public bool booted = false;
    public TextMeshProUGUI texter;
    public AudioClip pcBootSound;
    public AudioClip pcShutdownSound;

    public string sessionID = "";

    bool createID = true;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        off.SetActive(true);
        blackBars.SetActive(false);
        loadingSession.SetActive(false);
        shuttingDown.SetActive(false);
        operativeSystem.SetActive(false);


        exitBtn.SetActive(true);
        Debug.Log("start thing done");
    }
    void PlaySound(AudioClip clip)
    {
        if (hardDrive.curcase.pcAudio != null)
            hardDrive.curcase.pcAudio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {



        if (operativeSystemType != OperativeSystem.No_Boot_Device)
        {
            if (startSequence)
            {
                if (hardDrive.curcase != null)
                {
                    if (hardDrive.curcase.isPcON)
                    {



                        startSequence = false;
                        blackBars.SetActive(true);

                        Invoke("loadingScreen", 1.5f);
                    }
                    else
                    {

                    }
                }
                exitBtn.SetActive(false);
            }
        }
        else
        {
            if (startSequence)
            {
                if (hardDrive.curcase.isPcON)
                {
                    startSequence = false;
                    blackBars.SetActive(true);

                    Invoke("loadingScreen", 1.5f);
                }
                else
                {

                }
                exitBtn.SetActive(false);
            }
        }
        if (hardDrive.curcase != null)
        {
            if (!hardDrive.curcase.isPcON)
            {
                if (pickupController.isOnPCOS)
                {
                  
                    CloseOS();
                }
                if(hardDrive.curcase.currentMonitor != null)
                    hardDrive.curcase.currentMonitor.allowEnter = false;

                onBSOD = false;
                BSOD.SetActive(false);
                startSequence = true;
                black.SetActive(true);
                loadingOS.SetActive(false);
                operativeSystem.SetActive(false);
                loadingSession.SetActive(false);


                off.SetActive(true);
                blackBars.SetActive(false);
                shuttingDown.SetActive(false);
                booted = false;
                if (hardDrive.curcase.pcAudio != null)
                    hardDrive.curcase.pcAudio.Stop();

                //too lazy to optimize this

                string thinger = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                sessionID = "";
                for (int i = 0; i < 4; i++)
                {
                    sessionID += thinger[UnityEngine.Random.Range(0, thinger.Length)];
                }




            }
            else
            {
                off.SetActive(false);
                if(hardDrive.curcase.currentMonitor != null)
                    hardDrive.curcase.currentMonitor.allowEnter = true;
            }
        }
        else
        {
            onBSOD = false;
            BSOD.SetActive(false);
            startSequence = true;
            black.SetActive(true);


            off.SetActive(true);
            blackBars.SetActive(false);
        }
        if (onBSOD)
        {
            BSOD.SetActive(true);
        }
        else
        {
            BSOD.SetActive(false);
        }
        if (pickupController.isOnPCOS)
        {
            exitBtn.SetActive(true);
            hardDrive.canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        }
        else
        {
            if (hardDrive.curcase != null)
            {

                if (hardDrive.curcase.currentMonitor.monitorRatio == computerMonitor.Ratio.Square)
                    hardDrive.canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(960, 720);
                else
                    hardDrive.canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);

                exitBtn.SetActive(false);
            }
        }
        if (hardDrive.curcase != null)
        {

            if (hardDrive.curcase.currentMonitor.monitorRatio == computerMonitor.Ratio.Square)
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("OSScalable");

                foreach (GameObject thing in gameObjects)
                {
                    thing.GetComponent<RectTransform>().sizeDelta = new Vector2(960, 720);
                }
                blackBars.SetActive(true);





            }
            else
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("OSScalable");

                foreach (GameObject thing in gameObjects)
                {
                    thing.GetComponent<RectTransform>().sizeDelta = new Vector2(1280, 720);
                }
                blackBars.SetActive(false);




            }

        }

        if (shuttingDownBool)
        {

            operativeSystem.SetActive(false);
            black.SetActive(true);
            Invoke("tingle", 0.2f);

            //startSequence = true;


            shuttingDownBool = false;
        }
        if (booted)
        {

        }

    }
    void tingle()
    {
        black.SetActive(false);
        shuttingDown.SetActive(true);


        PlaySound(pcShutdownSound);
    }
    void loadingScreen()
    {
        if (hardDrive.curcase.isPcON)
        {

            if (!onBSOD)
            {
                off.SetActive(false);

                if (operativeSystemType != OperativeSystem.No_Boot_Device)
                {
                    black.SetActive(false);
                    float timeme = (100 / hardDrive.curcase.cpu.GetComponent<objectScript>().objSpeed) / 17;
                    if (hardDrive.curcase.ram1 != null && hardDrive.curcase.ram2 != null)
                    {
                        timeme += (1000 / hardDrive.curcase.ram1.GetComponent<objectScript>().objSpeed) / 2;
                        timeme += (4096 / hardDrive.curcase.ram1.GetComponent<objectScript>().ramSize) / 24;
                        timeme += (1000 / hardDrive.curcase.ram2.GetComponent<objectScript>().objSpeed) / 2;
                        timeme += (4096 / hardDrive.curcase.ram2.GetComponent<objectScript>().ramSize) / 24;
                    }
                    else if (hardDrive.curcase.ram1 != null && hardDrive.curcase.ram2 == null)
                    {
                        timeme += (1000 / hardDrive.curcase.ram1.GetComponent<objectScript>().objSpeed);
                        timeme += (4096 / hardDrive.curcase.ram1.GetComponent<objectScript>().ramSize) / 20;
                    }
                    else if (hardDrive.curcase.ram1 == null && hardDrive.curcase.ram2 != null)
                    {
                        timeme += (1000 / hardDrive.curcase.ram2.GetComponent<objectScript>().objSpeed);
                        timeme += (4096 / hardDrive.curcase.ram2.GetComponent<objectScript>().ramSize) / 20;
                    }


                    loadingOS.SetActive(true);
                    Debug.Log("Time: " + timeme);
                    StartCoroutine(delaying(sessionID, timeme));
                }
                else
                {
                    operativeSystem.SetActive(true);
                    black.SetActive(false);
                    booted = true;
                    Debug.Log("reached thinger dingle no hard drive");
                }
            }
        }
    }
    public IEnumerator delaying(string curID, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (sessionID == curID)
        {
            delayOs();
        }
        else
        {
            Debug.Log("not current session");
        }
    }
    void delayOs()
    {
        if (!onBSOD)
        {
            if (hardDrive.curcase.isPcON)
            {
                if (!booted)
                {
                    loadingOS.SetActive(false);
                    black.SetActive(true);
                    StartCoroutine(setOS());
                }
            }
        }
    }
    public IEnumerator setOS()
    {
        yield return new WaitForSeconds(1f);


        if (hardDrive.curcase != null)
        {
            if (hardDrive.curcase.currentMonitor.monitorRatio == computerMonitor.Ratio.Square)
            {
                blackBars.SetActive(true);
            }
            else
            {
                blackBars.SetActive(false);
            }
        }
        loadingOS.SetActive(false);
        black.SetActive(false);
        loadingSession.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        PlaySound(pcBootSound);
        float timeme = 0f;
        if (operativeSystemType == OperativeSystem.Tinglows_XP)
        {
            timeme = (100 / hardDrive.curcase.cpu.GetComponent<objectScript>().objSpeed) / 16f;
        }
        else if (operativeSystemType == OperativeSystem.Tinglows_2000)
        {
            timeme = (100 / hardDrive.curcase.cpu.GetComponent<objectScript>().objSpeed) / 20f;
        }
        else
        {
            timeme = (100 / hardDrive.curcase.cpu.GetComponent<objectScript>().objSpeed) / 20f;
        }
        if (timeme < 5f)
            timeme = 5f;
        yield return new WaitForSeconds(timeme);
        loadingSession.SetActive(false);






        operativeSystem.SetActive(true);
        booted = true;


        Debug.Log("os loaded actived thinger");

    }
    public void CloseOS()
    {
        if (hardDrive.curcase.currentMonitor.monitorRatio == computerMonitor.Ratio.Square)
            hardDrive.canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(960, 720);
        hardDrive.displayCam.targetTexture = hardDrive.displayCam.gameObject.GetComponentInParent<computerMonitor>().monitorTex;
        hardDrive.displayCam.targetDisplay = 1;
        pickupController.isOnPCOS = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }




}
