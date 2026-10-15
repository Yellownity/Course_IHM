using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;
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

        // Rendre persistant et s'abonner aux chargements de scène pour mettre à jour les objets VR/PC
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

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

        // Activer/désactiver les objets tagués dans la scène courante
        ActivateVROrPC();
    }

    private void ActivateVROrPC()
    {
        // Activer/désactiver tous les objets tagués "VR"
        try
        {
            var vrObjects = GameObject.FindGameObjectsWithTag("VR");
            foreach (var go in vrObjects)
            {
                if (go != null) go.SetActive(isVRActive);
            }

            var pcObjects = GameObject.FindGameObjectsWithTag("PC");
            foreach (var go in pcObjects)
            {
                if (go != null) go.SetActive(!isVRActive);
            }
        }
        catch (UnityException)
        {
            // Si les tags n'existent pas, GameObject.FindGameObjectsWithTag lance une exception.
            // On l'ignore pour éviter de casser l'exécution.
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // À chaque chargement de scène, appliquer l'état VR/PC
        ActivateVROrPC();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
