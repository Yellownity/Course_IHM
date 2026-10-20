using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    float timer = 0f;
    float startLapTime = 0f;
    float bestLapTime = float.MaxValue;
    List<float> checkpointTimes = new List<float>();
    List<float> bestCheckpointTimes = new List<float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Checkpoint.onCheckpointEvent += HandleCheckpointReached;
        StartCoroutine(Timer());
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
            //UIManager.SetTotalTime(timer);
            yield return new WaitForSeconds(0.01f);

        }
    }

    private void HandleCheckpointReached(object sender, int checkpointID)
    {

        float checkpointTime = timer - startLapTime;
        if (checkpointTimes.Count <= checkpointID)
        {
            checkpointTimes.Add(checkpointTime);
        }
        else
        {
            checkpointTimes[checkpointID] = checkpointTime;
        }
        float differenceTime = bestCheckpointTimes[checkpointID] - checkpointTime;

        if (checkpointID == 0)
        {

            // Lap finished, store the lap time
            float lapTime = timer - startLapTime;
            if (lapTime < bestLapTime)
            {
                bestLapTime = lapTime;
                bestCheckpointTimes = new List<float>(checkpointTimes);
                //UIManager.SetBestLapTime(bestLapTime);
            }
            startLapTime = timer; 
        }
        //UIManager.SetDifferenceTime(differenceTime);
    }


}
