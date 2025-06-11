using Unity.VisualScripting;
using UnityEngine;

public class objDetecter : MonoBehaviour
{
    public computerCase curcase;
    public ObjDetecterType thisObjType;
    public enum ObjDetecterType { Case, CPU, GPU, Motherboard, Power_Supply, Hard_Drive, RAM, CPU_Fan, DVD_Drive };

    [Header("HDD")]
    //if 0, hdd1, if 1, hdd2, if 2, hdd3
    public int hddIndex = 0;
    [Header("Slot")]
    //if 0, slot1, if 1, slot2, if 2, slot3
    public int caseSlotIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void PlaySound(int type)
    {
        if (type == 0)
        {
            if (pickupController.plySrc != null)
                pickupController.plySrc.PlayOneShot(pickupController.popop);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<objectScript>() != null)
        {
            if (other.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Motherboard && thisObjType == ObjDetecterType.Motherboard)
            {
                if (!curcase.hasMOBO)
                {
                    other.gameObject.transform.parent = this.gameObject.transform;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                    other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                    curcase.hasMOBO = true;
                    other.gameObject.GetComponent<objectScript>().isOnPC = true;
                    other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                    for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                    {
                        other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                    }
                    if (pickupController.heldObj == other.gameObject)
                    {
                        pickupController.heldObj = null;
                        pickupController.pickedObject = false;
                        pickupController.heldObjRB = null;
                    }
                    curcase.mobo = other.gameObject;
                    PlaySound(0);
                }
                else
                {
                    Debug.Log("already has a mobo!");
                }
            }
            if (other.gameObject.GetComponent<objectScript>().type == objectScript.CompType.DVD_Drive && thisObjType == ObjDetecterType.DVD_Drive)
            {
                if (!curcase.hasDVDDrive)
                {
                    other.gameObject.transform.parent = this.gameObject.transform;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, -90f);
                    other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                    curcase.hasDVDDrive = true;
                    other.gameObject.GetComponent<objectScript>().isOnPC = true;
                    other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                    for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                    {
                        other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                    }
                    if (pickupController.heldObj == other.gameObject)
                    {
                        pickupController.heldObj = null;
                        pickupController.pickedObject = false;
                        pickupController.heldObjRB = null;
                    }
                    curcase.dvdDrive = other.gameObject;
                    other.gameObject.GetComponent<DiskDrive>().curcase = curcase;
                    PlaySound(0);
                }
                else
                {
                    Debug.Log("already has a dvd drive!");
                }
            }
            if (other.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Power_Supply && thisObjType == ObjDetecterType.Power_Supply)
            {
                if (!curcase.hasPowerSupply)
                {
                    other.gameObject.transform.parent = this.gameObject.transform;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                    other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                    curcase.hasPowerSupply = true;
                    other.gameObject.GetComponent<objectScript>().isOnPC = true;
                    other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                    for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                    {
                        other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                    }
                    if (pickupController.heldObj == other.gameObject)
                    {
                        pickupController.heldObj = null;
                        pickupController.pickedObject = false;
                        pickupController.heldObjRB = null;
                    }
                    curcase.powerSupply = other.gameObject;
                    PlaySound(0);
                }
                else
                {
                    Debug.Log("already has a power supply!");
                }
            }
            if (other.gameObject.GetComponent<objectScript>().type == objectScript.CompType.Hard_Drive && thisObjType == ObjDetecterType.Hard_Drive)
            {
                if (hddIndex == 0)
                {
                    if (!curcase.hasHDD1)
                    {
                        other.gameObject.transform.parent = this.gameObject.transform;
                        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                        curcase.hasHDD1 = true;
                        other.gameObject.GetComponent<objectScript>().isOnPC = true;
                        other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                        for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                        {
                            other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                        }


                        if (pickupController.heldObj == other.gameObject)
                        {
                            pickupController.heldObj = null;
                            pickupController.pickedObject = false;
                            pickupController.heldObjRB = null;
                        }
                        curcase.hdd1 = other.gameObject;
                        curcase.hddList.Add(other.gameObject);
                        PlaySound(0);
                    }
                    else
                    {
                        Debug.Log("already has a hdd!");
                    }
                }
                else if (hddIndex == 1)
                {
                    if (!curcase.hasHDD2)
                    {
                        other.gameObject.transform.parent = this.gameObject.transform;
                        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                        curcase.hasHDD2 = true;
                        other.gameObject.GetComponent<objectScript>().isOnPC = true;
                        other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                        for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                        {
                            other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                        }
                        if (pickupController.heldObj == other.gameObject)
                        {
                            pickupController.heldObj = null;
                            pickupController.pickedObject = false;
                            pickupController.heldObjRB = null;
                        }
                        curcase.hdd2 = other.gameObject;
                        curcase.hddList.Add(other.gameObject);
                        PlaySound(0);
                    }
                    else
                    {
                        Debug.Log("already has a hdd!");
                    }
                }
                else if (hddIndex == 2)
                {
                    if (!curcase.hasHDD3)
                    {
                        other.gameObject.transform.parent = this.gameObject.transform;
                        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        other.gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.015f, gameObject.transform.position.z);
                        curcase.hasHDD3 = true;
                        other.gameObject.GetComponent<objectScript>().isOnPC = true;
                        other.gameObject.GetComponent<objectScript>().parent = this.gameObject.transform;
                        for (int i = 0; i < other.gameObject.GetComponents<Collider>().Length; i++)
                        {
                            other.gameObject.GetComponents<Collider>()[i].excludeLayers = curcase.caseLayer;
                        }
                        if (pickupController.heldObj == other.gameObject)
                        {
                            pickupController.heldObj = null;
                            pickupController.pickedObject = false;
                            pickupController.heldObjRB = null;
                        }
                        curcase.hdd3 = other.gameObject;
                        curcase.hddList.Add(other.gameObject);
                        PlaySound(0);
                    }
                    else
                    {
                        Debug.Log("already has a hdd!");
                    }
                }

            }
            
        }
    }
}
