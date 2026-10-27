using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
        Time.timeScale = 0f; // Pause the game
    }
    void Start()
    {
        
    }

    public void OnRestartPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void OnBackToMenuPressed()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnResumePressed()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

}
