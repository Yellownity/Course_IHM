using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using System.Collections;
using System.Collections.Generic;

public class ManagerVR : MonoBehaviour
{
    [SerializeField]
    private GameObject vrRig;
    [SerializeField]
    private GameObject desktopCamera;
    public static bool isVRActive = false;

    IEnumerator Start()
    {
        // Attendre un frame pour permettre l'initialisation du loader XR
        yield return null;

        Debug.Log($"ManagerVR Start: XRSettings.isDeviceActive={XRSettings.isDeviceActive}. vrRig={(vrRig?vrRig.name:"null")}, desktopCamera={(desktopCamera?desktopCamera.name:"null")}");

        bool xrActive = XRGeneralSettings.Instance?.Manager?.activeLoader != null;

        if (!xrActive)
        {
            // Vérifier si un XRDisplaySubsystem est en cours d'exécution
            var displays = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems(displays);
            foreach (var d in displays)
            {
                if (d.running)
                {
                    xrActive = true;
                    break;
                }
            }
        }

        if (xrActive)
        {
            if (vrRig) vrRig.SetActive(true);

            if (desktopCamera)
            {
                var cam = desktopCamera.GetComponent<Camera>();
                if (cam) cam.enabled = false; else desktopCamera.SetActive(false);
            }

            if (vrRig)
            {
                var vrCam = vrRig.GetComponentInChildren<Camera>();
                if (vrCam) vrCam.enabled = true;
            }
        }
        else
        {
            if (vrRig)
            {
                var vrCam = vrRig.GetComponentInChildren<Camera>();
                if (vrCam) vrCam.enabled = false;
                vrRig.SetActive(false);
            }

            if (desktopCamera)
            {
                var cam = desktopCamera.GetComponent<Camera>();
                if (cam) cam.enabled = true; else desktopCamera.SetActive(true);
            }
        }

        isVRActive = xrActive;
    }
}
