using Microsoft.Win32.SafeHandles;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject playMenuButton;


    private ColorBlock colors;

    [Header("PLay Menu")]

    [SerializeField] private GameObject mapNameButton;
    [SerializeField] private GameObject lapCountButton;
    private TextMeshProUGUI lapCountText;
    [SerializeField] private float initialRepeatDelay = 0.4f;
    [SerializeField] private float repeatInterval = 0.15f;

    [SerializeField] private GameObject plus;
    [SerializeField] private GameObject minus;
    [SerializeField] private GameObject next;
    [SerializeField] private GameObject previous;
    private Coroutine buttonColorCoroutine;

    private bool directionHeld;
    private int heldDirection;
    private float nextRepeatTime;

    [SerializeField] private GameObject playMenu;

    [SerializeField] private List<string> mapName;
    [SerializeField] private TextMeshProUGUI mapNameText;

    [Header("Controls Menu")]
    [SerializeField] private GameObject controlsBackButton;
    [SerializeField] private GameObject controlsMenu;
    private int currentMapIndex = 0;

    private int lapCount = 1;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(playMenuButton);
        lapCountText = lapCountButton.GetComponent<TextMeshProUGUI>();
        setLapCountText();
        playMenu.SetActive(false);
        controlsMenu.SetActive(false);
        mapNameText.text = mapName[currentMapIndex];
        colors = lapCountButton.GetComponent<Button>().colors;
    }

    // Update is called once per frame
    void Update()
    {
        if (!directionHeld || Time.unscaledTime < nextRepeatTime)
        {
            return;
        }

        MoveSelection();

        nextRepeatTime = Time.unscaledTime + repeatInterval;
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
        EventSystem.current.SetSelectedGameObject(mapNameButton);
        playMenu.SetActive(true);
    }
    private void changeNumberLap(int value)
    {
        GameObject button;
        bool red = false;
        if (currentMapIndex == 0)
        {
            red = true;
            return;
        }
        if (value > 0)
        {
            button = plus;
        }
        else
        {
            button = minus;
        }
        lapCount += value;
        if (lapCount == 0)
        {
            lapCount = 1;
            red = true;
        }
        lapCountText.text = lapCount.ToString();
        ShowButtonColor(button, red);
    }
    public void onBackPressed()
    {
        EventSystem.current.SetSelectedGameObject(playMenuButton);
        playMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    private void changeMap(int value)
    {
        currentMapIndex = (currentMapIndex + value + mapName.Count) % mapName.Count;
        mapNameText.text = mapName[currentMapIndex];
        if (value > 0)
        {
            ShowButtonColor(next, false);
        }
        else
        {
            ShowButtonColor(previous, false);
        }

        if (currentMapIndex == 0)
        {

            lapCount = 1;
            setLapCountText();
            colors.normalColor = new Color(0.5f, 0f, 0f, 1f);
            colors.selectedColor = new Color(0.8f, 0f, 0f, 1f);
            lapCountButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            if (lapCount == 1)
            {
                lapCount = 3;
                setLapCountText();
            }
            colors.normalColor = Color.black;
            colors.selectedColor = new Color(125f / 255f, 120f / 255f, 43f / 255f, 1f);
            lapCountButton.GetComponent<Button>().colors = colors;
        }
    }
    public void onControlsPressed()
    {
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
        controlsMenu.SetActive(true);
    }
    public void onRightLeftPressed(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            directionHeld = false;
            heldDirection = 0;
            return;
        }

        if (!context.performed)
        {
            return;
        }

        float direction = context.ReadValue<float>();

        // Ignore les petits mouvements involontaires du stick
        if (Mathf.Abs(direction) < 0.5f)
        {
            return;
        }

        int newDirection = direction > 0f ? 1 : -1;

        // Évite de réinitialiser le délai à chaque événement répété
        if (directionHeld && heldDirection == newDirection)
        {
            return;
        }

        directionHeld = true;
        heldDirection = newDirection;

        // Premier déplacement immédiat
        MoveSelection();

        // Attend avant de commencer le défilement automatique
        nextRepeatTime = Time.unscaledTime + initialRepeatDelay;
    }
    private void MoveSelection()
    {
        if (EventSystem.current.currentSelectedGameObject == mapNameButton)
        {
            changeMap(heldDirection);
        }
        else if (EventSystem.current.currentSelectedGameObject == lapCountButton)
        {
            changeNumberLap(heldDirection);
        }
    }
    private void setLapCountText() { lapCountText.text = lapCount.ToString(); }
    private IEnumerator changeButtonColor(GameObject button, bool red)
    {
        Color color = new Color(125f / 255f, 120f / 255f, 43f / 255f, 1f);

        if (red)
        {
            color = new Color(0.8f, 0f, 0f, 1f);
        }

        button.GetComponent<Image>().color = color;

        yield return new WaitForSecondsRealtime(repeatInterval);

        button.GetComponent<Image>().color = Color.black;
        buttonColorCoroutine = null;
    }

    private void ShowButtonColor(GameObject button, bool red)
    {
        if (buttonColorCoroutine != null)
        {
            StopCoroutine(buttonColorCoroutine);
        }

        buttonColorCoroutine = StartCoroutine(changeButtonColor(button, red));
    }
}
