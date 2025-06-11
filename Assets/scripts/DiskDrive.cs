using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiskDrive : MonoBehaviour
{
    public enum DiscDriveType
    {
        DVD,
        CD,
        BLURAY
    }
    public DiscDriveType driveType;
    public enum ReadSpeed
    {
        X1,
        X2,
        X4,
        X8,
        X16,
        X32,
        X64
    }
    public bool diskDriveOpened = false;
    public ReadSpeed readSpeed;
    public computerCase curcase;
    public GameObject onLight;
    public GameObject offLight;
    public GameObject diskDriveTrayObj;
    public Animator diskDriveTrayAnim;
    public GameObject currentDisc;
    public bool hasDisc = false;
    public bool discIsOnTray = false;
    void Update()
    {
        if (curcase != null)
        {
            if (curcase.isPcON == true)
            {
                if (diskDriveOpened)
                {
                    onLight.SetActive(true);
                    offLight.SetActive(false);
                }
                else
                {
                    onLight.SetActive(false);
                    offLight.SetActive(true);
                }
            }
            else
            {
                diskDriveOpened = false;
                onLight.SetActive(false);
                offLight.SetActive(false);
            }
        }
        else
        {
            diskDriveOpened = false;
            offLight.SetActive(false);
            onLight.SetActive(false);
        }
        if (diskDriveOpened)
        {
            diskDriveTrayAnim.SetBool("opened", true);
            if (discIsOnTray)
            {

                Invoke("DeFreezeDisc", 1f);
            }
            hasDisc = false;
        }
        else
        {
            diskDriveTrayAnim.SetBool("opened", false);
            if (discIsOnTray)
            {
                if (currentDisc != null)
                {
                    hasDisc = true;
                    currentDisc.gameObject.transform.parent = diskDriveTrayObj.gameObject.transform;
                    currentDisc.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    currentDisc.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    currentDisc.gameObject.GetComponent<Collider>().enabled = false;
                    currentDisc.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                    currentDisc.gameObject.transform.localScale = new Vector3(1.424831f, 1.424831f, 1.424831f);
                    currentDisc.gameObject.transform.position = new Vector3(diskDriveTrayObj.transform.position.x, diskDriveTrayObj.transform.position.y + 0.03f, diskDriveTrayObj.transform.position.z);
                }
                else
                {
                    hasDisc = false;
                }
            }
            else
            {
                hasDisc = false;
            }


        }
    }
    void DeFreezeDisc()
    {
        currentDisc.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
        currentDisc.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentDisc.gameObject.GetComponent<Collider>().enabled = true;




    }
}