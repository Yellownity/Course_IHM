using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;
    [Header("Controls Menu")]
    [SerializeField] private GameObject controlsBackButton;
    [SerializeField] private GameObject controlsMenu;

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

    public void OnControlsPressed()
    {
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
        controlsMenu.SetActive(true);
    }
    public void onBackPressed()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
        controlsMenu.SetActive(false);
    }

}
