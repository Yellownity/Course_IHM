using NUnit.Framework;
using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class LapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkpoints;
    private int currentCheckpoint = 0;
    private int totalCheckpoints;

    public static event EventHandler<GameObject> onCheckpointEvent;

    public static event EventHandler onLapFinishedEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Checkpoint.onCheckpointEvent += HandleCheckpointReached;
        totalCheckpoints = checkpoints.Count;
        if (onCheckpointEvent != null)
        {
            onCheckpointEvent(this, checkpoints[1]);
        }
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
            if(onCheckpointEvent != null)
            {
                int nextCheckpointIndex = (currentCheckpoint + 1) % totalCheckpoints;
                onCheckpointEvent(this, checkpoints[nextCheckpointIndex]);
            }
            if (currentCheckpoint == 0 && onLapFinishedEvent != null)
            {
                onLapFinishedEvent(this, EventArgs.Empty);
            }
        }
    }

    public void OnRespawnPressed()
    {
        Transform player = FindFirstObjectByType<PlayerMovement>().transform;
        player.position = checkpoints[currentCheckpoint].transform.position;
        player.rotation = checkpoints[currentCheckpoint].transform.rotation;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
