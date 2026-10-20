using NUnit.Framework;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject playMenuButton;

    [SerializeField] private TextMeshProUGUI lapCountText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject plusButton;
    private ColorBlock colors;
     

    [SerializeField] private GameObject playMenu;

    [SerializeField] private List<string> mapName;
    [SerializeField] private TextMeshProUGUI mapNameText;
    private int currentMapIndex = 0;

    private int lapCount = 1;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(playMenuButton);
        lapCountText.text = lapCount.ToString();
        playMenu.SetActive(false);
        mapNameText.text = mapName[currentMapIndex];
        colors = plusButton.GetComponent<Button>().colors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartPressed()
    {
        playMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        GameManager.Instance.totalLapCount = lapCount;
        SceneManager.LoadScene(mapName[currentMapIndex]);
    }

    public void onQuitPressed()
    {
        Application.Quit();
    }
    public void onPlayPressed()
    { 
        EventSystem.current.SetSelectedGameObject(nextButton);
        playMenu.SetActive(true);
        colors.normalColor = new Color(0.5f, 0f, 0f, 1f);
        colors.selectedColor = new Color(0.8f, 0f, 0f, 1f);
        plusButton.GetComponent<Button>().colors = colors;
    }
    public void onMoreLapsPressed()
    {
        if (currentMapIndex == 0)
        {
            return;
        }
        lapCount++;
        lapCountText.text = lapCount.ToString();
    }
    public void onLessLapsPressed()
    {
        if (lapCount > 1)
        {
            lapCount--;
            lapCountText.text = lapCount.ToString();
        }
    }
    public void onBackPressed()
    {
        EventSystem.current.SetSelectedGameObject(playMenuButton);
        playMenu.SetActive(false);
    }

    public void onNextPressed()
    {
        currentMapIndex = (currentMapIndex + 1) % mapName.Count;
        mapNameText.text = mapName[currentMapIndex];
        if (currentMapIndex == 0)
        {

            lapCount = 1;
            lapCountText.text = lapCount.ToString();
            colors.normalColor = new Color(0.5f, 0f, 0f, 1f);
            colors.selectedColor = new Color(0.8f, 0f, 0f, 1f);
            plusButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            colors.normalColor = Color.black;
            colors.selectedColor= new Color(125f / 255f, 120f / 255f, 43f / 255f, 1f);
            plusButton.GetComponent<Button>().colors = colors;
        }
    }
    public void onPreviousPressed()
    {
        currentMapIndex = (currentMapIndex - 1 + mapName.Count) % mapName.Count;
        mapNameText.text = mapName[currentMapIndex];
        if (currentMapIndex == 0)
        {
            lapCount=1;
            lapCountText.text = lapCount.ToString();
            colors.normalColor = new Color(0.5f, 0f, 0f, 1f);
            colors.selectedColor = new Color(0.8f, 0f, 0f, 1f);
            plusButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            colors.normalColor = Color.black;
            colors.selectedColor = new Color(125f / 255f, 120f / 255f, 43f / 255f, 1f);
            plusButton.GetComponent<Button>().colors = colors;
        }
    }
}
