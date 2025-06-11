using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiscScript : MonoBehaviour
{
    public enum DiscType
    {
        DVD,
        CD,
        BLURAY
    }
    public DiscType discType;
    public enum DiscStorageType
    {
        OSInstaller,
        Storage,
        Game
    }
    public DiscStorageType discStorageType;
    public GameObject installerUI;
    public GameObject installedFiles;
}