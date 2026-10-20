using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private GameObject quitButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(restartButton);
    }
    void Start()
    {
        
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
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

    void Update()
    {
        
    }
}
