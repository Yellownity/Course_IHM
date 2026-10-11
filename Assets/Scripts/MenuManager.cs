using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject playMenuButton;

    [SerializeField] private TextMeshProUGUI lapCountText;
    [SerializeField] private GameObject plusButton;

    [SerializeField] private GameObject playMenu;

    int lapCount = 3;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(playMenuButton);
        lapCountText.text = lapCount.ToString();
        playMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartPressed()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (ManagerVR.isVRActive)
        {
            SceneManager.LoadScene("CircuitVR");
        }
        else
        {
            SceneManager.LoadScene("CircuitPC");
        }
    }

    public void onQuitPressed()
    {
        Application.Quit();
    }
    public void onPlayPressed()
    { 
        EventSystem.current.SetSelectedGameObject(plusButton);
        playMenu.SetActive(true);
    }
    public void onMoreLapsPressed()
    {
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
}
