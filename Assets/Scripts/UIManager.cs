using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int lapsCompleted = 0;
    [SerializeField] private int totalLaps = 3;
    [SerializeField] private TextMeshProUGUI lapNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LapManager.onLapFinishedEvent += HandleLapFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleLapFinished(object sender,EventArgs args)
    {
        lapsCompleted += 1;
        lapNumber.text = "Lap: " + lapsCompleted + "/" + totalLaps;

        Debug.Log("Total laps completed: " + lapsCompleted);
    }
}
