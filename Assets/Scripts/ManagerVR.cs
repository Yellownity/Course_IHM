using UnityEngine;
using UnityEngine.XR;

public class ManagerVR : MonoBehaviour
{
    [SerializeField]
    private GameObject vrRig;
    [SerializeField]
    private GameObject desktopCamera;

    void Awake()
    {
        if (XRSettings.isDeviceActive)
        {
            vrRig.SetActive(true);
            desktopCamera.SetActive(false);
        }
        else
        {
            vrRig.SetActive(false);
            desktopCamera.SetActive(true);
        }
    }
}
