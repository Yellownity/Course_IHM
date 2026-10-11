using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform CameraSpot;
    [SerializeField] private Vector3 offset = new Vector3(0, 50,0); 
    [SerializeField] private float smoothTime = 0.35f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (CameraSpot != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position,CameraSpot.position + offset, ref velocity, smoothTime);
        }
    }
}
