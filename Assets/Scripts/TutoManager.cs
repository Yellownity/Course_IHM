using UnityEngine;
using System.Collections.Generic;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutoPanels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disableAll();
        tutoPanels[0].SetActive(true);
        Checkpoint.onCheckpointEvent += HandleCheckpointReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void disableAll()
    {
        foreach (GameObject panel in tutoPanels)
        {
            panel.SetActive(false);
        }
    }
    void HandleCheckpointReached(object sender, int checkpointID)
    {
        if (checkpointID < tutoPanels.Count)
        {
            disableAll();
            tutoPanels[checkpointID].SetActive(true);
        }
    }
}
