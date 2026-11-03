using UnityEngine;
using System;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerMovement != null)
        {
            float zoomLevel;

            if (playerMovement.IsBoosting() && playerMovement.GetBoostAmount() > 0)
            {
                zoomLevel = Mathf.Lerp(mainCamera.fieldOfView, 45f, Time.deltaTime);
            }
            else
            {
                zoomLevel = Mathf.Lerp(mainCamera.fieldOfView, 60f, Time.deltaTime);
            }

            mainCamera.fieldOfView = zoomLevel;
        }
    }
}
