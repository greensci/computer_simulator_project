using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class pickupController : MonoBehaviour
{



    [Header("everything else")]
    public AudioSource audioSrc;
    public GameObject uiObj;
    public Image disarmModeImg;
    public Image linkModeImg;
    public Transform orgFollowPoint;
    public Transform followPoint;
    public Transform parent;
    public static GameObject heldObj;
    public static Rigidbody heldObjRB;
    public TextMeshProUGUI infotxt;
    public TextMeshProUGUI moneytxt;
    public LayerMask componentColliderExclude;

    public float pickupRange = 5.0f;
    public float pickupForce = 750.0f;
    public float objRotationForce = 250.0f;
    public float pickedObjDistance = 0.1f;
    public static bool pickedObject = false;
    public static bool objIsOnPC = false;
    public LayerMask objLayer;
    public static bool canRotate = false;
    public static bool disarmMode = false;
    RaycastHit hitted;

    [Header("Pc Parts")]
    public Transform moboParent;

    [Header("Pc OS stuff")]
    public static bool isOnPCOS = false;
    public pcOS currPCOS;
    public pcOS lastPCOS;
    public Material orgPcOSMat;
    public Material replacePcOSMat;
    public static bool linkmode = false;
    public int linkStep = 0;
    public Color clear;
    public LayerMask playerLayer;
    public static AudioSource plySrc;
    public AudioClip pop;
    public static AudioClip popop;

    void Start()
    {
        infotxt.text = "";
        currPCOS = null;
        popop = pop;
        plySrc = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        moneytxt.text = playerMove.playerMoney + "$";
        //if (GameControls.inPrimary)
        if (!isOnPCOS)
        {
            uiObj.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {

                if (heldObj == null)
                {

                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitted, pickupRange, objLayer))
                    {

                        //pickup obj
                        if (!disarmMode)
                        {
                            if (hitted.transform.gameObject.GetComponent<objectScript>().isOnPC == false)
                            {
                                objIsOnPC = false;
                                pickedObject = true;
                                for (int i = 0; i < hitted.transform.gameObject.GetComponents<Collider>().Length; i++)
                                {
                                    hitted.transform.gameObject.GetComponents<Collider>()[i].excludeLayers = playerLayer;
                                }

                            }
                            else
                            {
                                if (!disarmMode)
                                {
                                    objIsOnPC = true;
                                    pickedObject = true;
                                }
                            }
                        }
                        else
                        {
                            if (hitted.transform.gameObject.GetComponent<objectScript>().isOnPC == true)
                            {
                                hitted.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                                hitted.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                                if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Motherboard)
                                {
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasMOBO = false;
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.mobo = null;
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Power_Supply)
                                {
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasPowerSupply = false;
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.powerSupply = null;
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Hard_Drive)
                                {
                                    if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().hddIndex == 0)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasHDD1 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hdd1 = null;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hddList.Remove(hitted.transform.gameObject);
                                    }
                                    else if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().hddIndex == 1)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasHDD2 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hdd2 = null;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hddList.Remove(hitted.transform.gameObject);
                                    }
                                    else if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().hddIndex == 2)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasHDD3 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hdd3 = null;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hddList.Remove(hitted.transform.gameObject);
                                    }
                                    hitted.transform.gameObject.GetComponentInChildren<pcOS>().hardDrive.displayCam = null;

                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.CPU)
                                {
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasCPU = false;
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.cpu = null;
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.RAM)
                                {
                                    if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().ramSlot == 1)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasRAM1 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.ram1 = null;
                                    }
                                    else if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().ramSlot == 2)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasRAM2 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.ram2 = null;
                                    }
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.CPU_Fan)
                                {
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasCPUFan = false;
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.cpuFan = null;
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.DVD_Drive)
                                {
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.hasDVDDrive = false;
                                    hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<objDetecter>().curcase.dvdDrive = null;
                                    hitted.transform.gameObject.GetComponent<DiskDrive>().curcase = null;
                                }
                                else if (hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.GPU)
                                {
                                    if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().gpuSlot == 1)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasGPU1 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.gpu1 = null;
                                    }
                                    else if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().gpuSlot == 2)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasGPU2 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.gpu2 = null;
                                    }
                                    else if (hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().gpuSlot == 3)
                                    {
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.hasGPU3 = false;
                                        hitted.transform.gameObject.GetComponent<objectScript>().parent.transform.gameObject.GetComponent<moboObjDetecter>().curMobo.gpu3 = null;
                                    }

                                }
                                hitted.transform.gameObject.GetComponent<objectScript>().isOnPC = false;

                                for (int i = 0; i < hitted.transform.gameObject.GetComponents<Collider>().Length; i++)
                                {
                                    hitted.transform.gameObject.GetComponents<Collider>()[i].excludeLayers = componentColliderExclude;
                                }
                                objIsOnPC = false;
                                pickedObject = true;

                            }
                            else
                            {

                                objIsOnPC = false;
                                pickedObject = true;

                            }
                        }

                    }
                }
                else
                {
                    pickedObject = false;
                }

            }
        }
        else
        {
            uiObj.SetActive(false);
        }
        if (!pickedObject)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitted, pickupRange, objLayer))
                {
                    if (hitted.transform.gameObject.GetComponentInParent<DiskDrive>() != null)
                    {
                        hitted.transform.gameObject.GetComponentInParent<DiskDrive>().diskDriveOpened = !hitted.transform.gameObject.GetComponentInParent<DiskDrive>().diskDriveOpened;
                        UnityEngine.Debug.Log("disk drive activatateted");
                    }

                    else if (hitted.transform.gameObject.GetComponentInParent<computerCase>() != null)
                    {
                        if (hitted.transform.gameObject.GetComponentInParent<computerCase>().isPcON)
                        {
                            hitted.transform.gameObject.GetComponentInParent<computerCase>().shutdownPc();
                        }
                        else
                        {
                            if (hitted.transform.gameObject.GetComponentInParent<computerCase>().hasMOBO)
                            {
                                if (hitted.transform.gameObject.GetComponentInParent<computerCase>().hasCPU)
                                {
                                    if (hitted.transform.gameObject.GetComponentInParent<computerCase>().hasPowerSupply)
                                    {
                                        if (!hitted.transform.gameObject.GetComponentInParent<computerCase>().isPcON)
                                        {

                                            hitted.transform.gameObject.GetComponentInParent<computerCase>().isPcON = true;
                                        }
                                    }
                                    else
                                    {
                                        StartCoroutine(main.setErrorMessage("Can't turn on the PC without a Power Supply!"));
                                        Debug.Log("Can't turn on the PC without a Power Supply!");
                                    }
                                }
                                else
                                {
                                    StartCoroutine(main.setErrorMessage("Can't turn on the PC without a CPU!"));
                                    Debug.Log("Can't turn on the PC without a CPU!");
                                }
                            }
                            else
                            {
                                StartCoroutine(main.setErrorMessage("Can't turn on the PC without a Motherboard!"));
                                Debug.Log("Can't turn on the PC without a Motherboard!");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("this is not a computer!");
                        StartCoroutine(main.setErrorMessage("This is not a computer!"));
                    }
                }
            }

            if (GameControls.inSecondary)
            {


                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitted, pickupRange, objLayer))
                {

                    if (hitted.transform.gameObject.tag == "ComputerOS")
                    {
                        if (hitted.transform.gameObject.GetComponent<computerMonitor>().onLeComputahr == true)
                        {
                            if (hitted.transform.gameObject.GetComponent<computerMonitor>().allowEnter == true)
                            {


                                /*if (hitted.transform.GetComponent<computerMonitor>().monitorScreen.material != replacePcOSMat)
                                {
                                    //hitted.transform.gameObject.GetComponentInChildren<Camera>().targetTexture = pcOSTex;
                                    //hitted.transform.gameObject.GetComponentInChildren<Camera>().targetTexture = null;
                                    hitted.transform.gameObject.GetComponentInChildren<Camera>().targetDisplay = 0;
                                    */
                                //hitted.transform.GetComponent<computerMonitor>().currPCOS = hitted.transform.gameObject.GetComponentInChildren<Camera>();
                                isOnPCOS = true;
                                Cursor.visible = true;
                                Cursor.lockState = CursorLockMode.None;
                                hitted.transform.GetComponent<computerMonitor>().monitorCam.targetTexture = null;
                                hitted.transform.GetComponent<computerMonitor>().monitorCam.targetDisplay = 0;

                                Debug.Log("entered to os!");
                            }
                        }


                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (linkmode)
                {
                    linkmode = false;
                    linkStep = 0;
                    StartCoroutine(main.setInfoMessage("Link Mode deactivated."));
                }
                else
                {
                    linkmode = true;
                    StartCoroutine(main.setInfoMessage("Link Mode activated.\nChoose a computer."));
                }


            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (disarmMode)
                {
                    disarmMode = false;
                    StartCoroutine(main.setInfoMessage("Disarm Mode deactivated."));
                }
                else
                {
                    StartCoroutine(main.setInfoMessage("Disarm Mode activated."));
                    disarmMode = true;
                }
            }
        }
        if (linkmode)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitted, pickupRange, objLayer))
                {
                    if (linkStep == 0)
                    {
                        if (hitted.transform.gameObject.GetComponent<computerCase>() != null)
                        {
                            if (hitted.transform.gameObject.GetComponentInChildren<moboScript>() != null)
                            {
                                currPCOS = hitted.transform.gameObject.GetComponent<computerCase>().pcOS;
                                linkStep++;
                                StartCoroutine(main.setInfoMessage("Now, choose a object to connect."));
                            }
                            else
                            {
                                Debug.Log("this is pc doesnt have a mobo!");
                                StartCoroutine(main.setErrorMessage("This Case doesn't have a Motherboard!"));
                                linkStep = 0;
                            }



                        }
                        else if (hitted.transform.gameObject.GetComponent<moboScript>() != null)
                        {
                            if (hitted.transform.gameObject.GetComponentInParent<computerCase>() != null)
                            {
                                currPCOS = hitted.transform.gameObject.GetComponentInParent<computerCase>().pcOS;
                                linkStep++;
                                StartCoroutine(main.setInfoMessage("Now, choose a object to connect."));
                            }
                            else
                            {
                                Debug.Log("this is mobo is not on a pc!");
                                StartCoroutine(main.setErrorMessage("This Motherboard is not on a computer!"));
                                linkStep = 0;
                            }

                        }
                        else
                        {
                            Debug.Log("this is not a computer!");
                            StartCoroutine(main.setErrorMessage("This is not a computer!"));
                            linkStep = 0;
                        }

                    }
                    else if (linkStep == 1)
                    {


                        if (hitted.transform.gameObject.GetComponent<computerMonitor>() != null)
                        {


                            computerMonitor monitor = hitted.transform.gameObject.GetComponent<computerMonitor>();


                            computerCase[] computers = FindObjectsOfType<computerCase>();



                            foreach (computerCase cp in computers)
                            {
                                if (cp.currentMonitor == monitor)
                                {
                                    monitor.currpcOS = null;
                                    cp.currentMonitor.clear = true;
                                    cp.currentMonitor.onLeComputahr = false;
                                    cp.currentMonitor.skipDiddy = false;
                                    if (cp.currentMonitor.currpcOS != null)
                                        cp.currentMonitor.currpcOS.hardDrive.canvas.worldCamera = null;
                                    cp.pcOS.hardDrive.canvas.worldCamera = null;
                                    cp.currentMonitor.currpcOS = null;
                                    cp.currentMonitor = null;

                                }
                            }


                            currPCOS.hardDrive.curcase.currentMonitor = monitor;






                            currPCOS.hardDrive.displayCam = monitor.gameObject.GetComponentInChildren<Camera>();
                            currPCOS.hardDrive.canvas.worldCamera = monitor.gameObject.GetComponentInChildren<Camera>();
                            currPCOS.hardDrive.curcase = currPCOS.GetComponentInParent<computerCase>();




                            monitor.monitorScreen.material = monitor.monitorMat;
                            currPCOS.gameObject.GetComponent<pcOS>().hardDrive.displayCam = monitor.monitorCam;
                            currPCOS.hardDrive.displayCam.targetTexture = monitor.monitorTex;

                            StartCoroutine(main.setInfoMessage("Monitor Connected!"));


                            linkStep++;
                        }
                        else if (hitted.transform.gameObject.GetComponent<AudioSource>() != null && hitted.transform.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Speaker)
                        {
                            computerCase[] computers = FindObjectsOfType<computerCase>();
                            foreach (computerCase cp in computers)
                            {
                                if (cp.pcAudio == hitted.transform.gameObject.GetComponent<AudioSource>())
                                {
                                    cp.pcAudio = null;


                                }
                            }

                            currPCOS.GetComponentInParent<computerCase>().pcAudio = hitted.transform.gameObject.GetComponent<AudioSource>();
                            currPCOS.GetComponentInParent<computerCase>().pcAudio = hitted.transform.gameObject.GetComponent<AudioSource>();
                            StartCoroutine(main.setInfoMessage("Speaker Connected!"));
                            linkStep++;

                        }
                        else
                        {
                            Debug.Log("this is not a valid obj!");
                            StartCoroutine(main.setErrorMessage("This is not a valid object!"));
                            linkStep = 0;
                        }

                    }
                    if (linkStep > 1)
                    {
                        linkmode = false;
                        linkStep = 0;
                    }

                }
            }
        }
        if (!GameControls.inPrimary)
        {
            pickedObject = false;

        }
        if (Input.GetKeyDown(KeyCode.F))
        {

            canRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
            canRotate = false;

        if (disarmMode)
        {
            disarmModeImg.color = Color.white;
        }
        else
        {
            disarmModeImg.color = clear;
        }
        if (linkmode)
        {

            linkModeImg.color = Color.white;

        }
        else
        {
            linkModeImg.color = clear;
            linkStep = 0;
        }





        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (mousewheel > 0)
        {

            followPoint.position += transform.forward * 0.25f;

        }
        if (mousewheel < 0)
        {
            followPoint.position -= transform.forward * 0.25f;

        }

        if (pickedObject)
        {
            if (hitted.transform.gameObject != null)
                PickupObject(hitted.transform.gameObject);

        }
        else
        {
            followPoint.position = orgFollowPoint.position;
            DropObject();
        }

        if (pickedObject || heldObj != null)
        {
            //move obj
            MoveObject();
        }
    }
    void MoveObject()
    {
        //followPoint.transform.position = heldObj.transform.position;
        if (Vector3.Distance(heldObj.transform.position, followPoint.position) > 0.1f)
        {

            Vector3 moveDirection = (followPoint.position - heldObj.transform.position) * pickedObjDistance;
            heldObjRB.AddForce(moveDirection * pickupForce);
            if (canRotate)
            {
                if (!objIsOnPC)
                {
                    Vector3 thingerer = new Vector3(0f, -GameControls.cameraAxis.x, -GameControls.cameraAxis.y);
                    heldObj.transform.RotateAround(followPoint.position, thingerer * objRotationForce * Time.deltaTime, 3.75f);
                }
                else
                {
                    Vector3 thingerer = new Vector3(0f, -GameControls.cameraAxis.x, -GameControls.cameraAxis.y);
                    if (heldObj.GetComponentInParent<computerCase>() != null)
                        heldObj.GetComponentInParent<computerCase>().gameObject.transform.RotateAround(followPoint.position, thingerer * objRotationForce * Time.deltaTime, 3.75f);
                    else if (heldObj.GetComponentInParent<moboScript>() != null)
                        heldObj.GetComponentInParent<moboScript>().gameObject.transform.RotateAround(followPoint.position, thingerer * objRotationForce * Time.deltaTime, 3.75f);


                }


            }

        }


    }
    void PickupObject(GameObject pickObj)
    {
        if (!objIsOnPC)
        {
            if (pickObj.GetComponent<Rigidbody>())
            {
                if (pickObj.GetComponent<objectScript>().isObjDamaged)
                    infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other + "\n<color=red>Damaged";
                else
                    infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other;

                heldObjRB = pickObj.GetComponent<Rigidbody>();

                heldObjRB.linearDamping = 10;
                if (canRotate)
                {
                    heldObjRB.constraints = RigidbodyConstraints.None;
                }
                else
                {
                    heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
                }
                heldObjRB.transform.parent = parent;
                heldObj = pickObj;

            }
        }
        else
        {
            if (!disarmMode)
            {
                if (pickObj.GetComponentInParent<computerCase>().gameObject.GetComponent<Rigidbody>())
                {
                    if (pickObj.GetComponent<objectScript>().isObjDamaged)
                        infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other + "\n<color=red>Damaged";
                    else
                        infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other;
                    heldObjRB = pickObj.GetComponentInParent<computerCase>().gameObject.GetComponent<Rigidbody>();

                    heldObjRB.linearDamping = 10;
                    if (canRotate)
                    {
                        heldObjRB.constraints = RigidbodyConstraints.None;
                    }
                    else
                    {
                        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
                    }
                    heldObjRB.transform.parent = parent;
                    heldObj = pickObj;

                }
                else if (pickObj.GetComponentInParent<moboScript>().gameObject.GetComponent<Rigidbody>())
                {
                    if (pickObj.GetComponent<objectScript>().isObjDamaged)
                        infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other + "\n<color=red>Damaged";
                    else
                        infotxt.text = pickObj.GetComponent<objectScript>().name + "\n" + pickObj.GetComponent<objectScript>().other;
                    heldObjRB = pickObj.GetComponentInParent<moboScript>().gameObject.GetComponent<Rigidbody>();

                    heldObjRB.linearDamping = 10;
                    if (canRotate)
                    {
                        heldObjRB.constraints = RigidbodyConstraints.None;
                    }
                    else
                    {
                        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
                    }
                    heldObjRB.transform.parent = parent;
                    heldObj = pickObj;
                }
            }
            else
            {
                if (pickObj.GetComponent<Rigidbody>())
                {
                    heldObjRB = pickObj.GetComponent<Rigidbody>();
                    heldObj = pickObj;
                }

            }

        }
    }
    void DropObject()
    {
        infotxt.text = "";

        if (heldObjRB != null && heldObj != null)
        {



            heldObjRB.useGravity = true;
            heldObjRB.linearDamping = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;
            heldObjRB.transform.gameObject.GetComponent<Collider>().excludeLayers = 0;
            heldObjRB.transform.parent = null;
            heldObj = null;
        }

    }
}
