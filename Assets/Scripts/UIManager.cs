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
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private TextMeshProUGUI bestLapTimeText;
    [SerializeField] private TextMeshProUGUI differenceTimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created



    // 2. Assigner l'instance et rendre l'objet persistant
    private void OnEnable()
    {
        LapManager.onLapFinishedEvent += HandleLapFinished;
        PlayerMovement.changingBoostUIEvent += HandleBoost;
        InputController.onPauseEvent += HandlePause;
    }
    private void OnDisable()
    {
        LapManager.onLapFinishedEvent -= HandleLapFinished;
        PlayerMovement.changingBoostUIEvent -= HandleBoost;
        InputController.onPauseEvent -= HandlePause;
    }

    void Start()
    {

        if (GameManager.Instance != null)
        {
            totalLaps = GameManager.Instance.totalLapCount;
        }
        else
        {
            totalLaps = 3;
        }
        ActualiseLapNumber();
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
        if (lapsCompleted >= totalLaps)
        {
            endGamePanel.SetActive(true);
        }
    }

    void ActualiseLapNumber()
    {
        lapNumber.text = "Lap: " + lapsCompleted + "/" + totalLaps;
    }

    public void SetTotalTime(float time)
    {
        timerText.text = "Time: " + time.ToString("F2") + "s";
    }
    public void SetBestLapTime(float time)
    {
        bestLapTimeText.text = "Best Lap: " + time.ToString("F2") + "s";
    }
    public void SetDifferenceTime(float time)
    {
        if (time > 0)
        {
            differenceTimeText.text = "+" + time.ToString("F2") + "s";
            differenceTimeText.color = Color.red;
        }
        else if (time < 0)
        {
            differenceTimeText.text = time.ToString("F2") + "s";
            differenceTimeText.color = Color.green;
        }
        else
        {
            differenceTimeText.text = "";
        }
    }
    void HandlePause(object sender, EventArgs args)
    {
        pausePanel.SetActive(true);
    }
}
