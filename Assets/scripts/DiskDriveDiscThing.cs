using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiskDriveDiscThing : MonoBehaviour
{
    public DiskDrive dd;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DiscScript>() != null)
        {
            if (!dd.discIsOnTray){
                dd.discIsOnTray = true;
                dd.currentDisc = other.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DiscScript>() != null)
        {
            if (dd.discIsOnTray){
                dd.discIsOnTray = false;
                 dd.currentDisc = null;
            }
        }
    }
}