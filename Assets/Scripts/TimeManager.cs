using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    float timer = 0f;
    float startLapTime = 0f;
    float bestLapTime = float.MaxValue;
    public List<float> checkpointTimes = new List<float>();
    List<float> bestCheckpointTimes = new List<float>();

    [SerializeField] private UIManager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Timer());
    }
    private void OnEnable()
    {
        Checkpoint.onCheckpointEvent += HandleCheckpointReached;
    }
    private void OnDisable()
    {
        Checkpoint.onCheckpointEvent -= HandleCheckpointReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            uiManager.SetTotalTime(timer);
            yield return new WaitForSeconds(0.01f);

        }
    }

    private void HandleCheckpointReached(object sender, int checkpointID)
    {

        float checkpointTime = timer - startLapTime;
        if (checkpointTimes.Count <= checkpointID)
        {
            for (int i = checkpointTimes.Count; i < checkpointID; i++)
            {
                checkpointTimes.Add(float.MaxValue);
            }
            checkpointTimes.Add(checkpointTime);
        }
        else
        {
            checkpointTimes[checkpointID] = checkpointTime;
        }
        if (bestCheckpointTimes.Count != 0)
        {
            float differenceTime = checkpointTime - bestCheckpointTimes[checkpointID];
            uiManager.SetDifferenceTime(differenceTime);
        }

        if (checkpointID == 0)
        {

            // Lap finished, store the lap time
            float lapTime = timer - startLapTime;
            if (lapTime < bestLapTime)
            {
                bestLapTime = lapTime;
                bestCheckpointTimes = new List<float>(checkpointTimes);
                uiManager.SetBestLapTime(bestLapTime);
            }
            startLapTime = timer; 
        }
    }


}
