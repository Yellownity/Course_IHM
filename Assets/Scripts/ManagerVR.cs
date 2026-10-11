using UnityEngine;
using UnityEngine.XR;

public class ManagerVR : MonoBehaviour
{
    [SerializeField]
    private GameObject vrRig;
    [SerializeField]
    private GameObject desktopCamera;
    public static bool isVRActive = false;

    void Awake()
    {
        if (XRSettings.isDeviceActive)
        {
            vrRig.SetActive(true);
            desktopCamera.SetActive(false);
            isVRActive = true;
        }
        else
        {
            vrRig.SetActive(false);
            desktopCamera.SetActive(true);
            isVRActive = false;
        }
    }
}
