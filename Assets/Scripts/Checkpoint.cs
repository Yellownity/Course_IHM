using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int checkpointID;
    public static event EventHandler<int> onCheckpointEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& onCheckpointEvent != null)
        {

            onCheckpointEvent(this, checkpointID);
        }
    }
}
