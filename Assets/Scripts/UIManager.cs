using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int lapsCompleted = 0;
    [SerializeField] private int totalLaps = 3;
    [SerializeField] private TextMeshProUGUI lapNumber;
    [SerializeField] private Slider boostSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LapManager.onLapFinishedEvent += HandleLapFinished;
        PlayerMovement.changingBoostUIEvent += HandleBoost;
    }

    private void OnDisable()
    {
        LapManager.onLapFinishedEvent -= HandleLapFinished;
        PlayerMovement.changingBoostUIEvent -= HandleBoost;
    }


    void HandleBoost(object sender, float boostAmount)
    {
        Debug.Log("boostAmount: " + boostAmount);
        boostSlider.value = boostAmount;
    }

    void HandleLapFinished(object sender,EventArgs args)
    {
        lapsCompleted += 1;
        lapNumber.text = "Lap: " + lapsCompleted + "/" + totalLaps;

        Debug.Log("Total laps completed: " + lapsCompleted);
    }
}
