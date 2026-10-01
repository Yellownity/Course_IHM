using System;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int totalCheckpoints = 3;
    private int currentCheckpoint = 0;

    public static event EventHandler onLapFinishedEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Checkpoint.onCheckpointEvent += HandleCheckpointReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleCheckpointReached(object sender, int checkpointID)
    {
        int nextCheckpoint = (currentCheckpoint + 1) % totalCheckpoints;
        if (checkpointID == nextCheckpoint)
        {
            currentCheckpoint = nextCheckpoint;
            if (currentCheckpoint == 0 && onLapFinishedEvent != null)
            {
                onLapFinishedEvent(this, EventArgs.Empty);
            }
        }
    }
}
