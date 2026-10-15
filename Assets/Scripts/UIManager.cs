using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int lapsCompleted = 0;
    [SerializeField] private int totalLaps;
    [SerializeField] private TextMeshProUGUI lapNumber;
    [SerializeField] private Slider boostSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LapManager.onLapFinishedEvent += HandleLapFinished;
        PlayerMovement.changingBoostUIEvent += HandleBoost;
        if (GameManager.Instance != null)
        {
            totalLaps = GameManager.Instance.totalLapCount;
        }
        else
        {
            totalLaps = 3;
        }
        ActualiseLapNumber();
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        LapManager.onLapFinishedEvent -= HandleLapFinished;
        PlayerMovement.changingBoostUIEvent -= HandleBoost;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2") + "s";
            yield return new WaitForSeconds(0.01f);

        }
    }


    void HandleBoost(object sender, float boostAmount)
    {
        Debug.Log("boostAmount: " + boostAmount);
        boostSlider.value = boostAmount;
    }

    void HandleLapFinished(object sender,EventArgs args)
    {
        lapsCompleted += 1;
        ActualiseLapNumber();
    }

    void ActualiseLapNumber()
    {
        lapNumber.text = "Lap: " + lapsCompleted + "/" + totalLaps;
    }
}
