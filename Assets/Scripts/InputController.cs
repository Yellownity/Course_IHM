using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    PlayerMovement playerMovement;


    float left_right_input;
    float forward;

    InputAction rightLeftAction;
    InputAction forwardAction;
    InputAction boostAction;

    InputAction driftAction;

    public static event EventHandler<EventArgs> onPauseEvent;



    private void Awake()
    {
        // 1. Vérifier si une instance existe déjà
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Détruit le doublon
            return;
        }

        // Keep the controller scoped to the player in the current scene.
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        rightLeftAction = playerInput.actions.FindAction("Player/Right_Left");
        forwardAction = playerInput.actions.FindAction("Player/Forward");
        boostAction = playerInput.actions.FindAction("Player/Boost");
        driftAction = playerInput.actions.FindAction("Player/Drift");

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float turnValue = rightLeftAction.ReadValue<float>();
        playerMovement.putTurn(turnValue);

        float forwardValue = forwardAction.ReadValue<float>();
        playerMovement.putForward(forwardValue);

        bool boostValue = boostAction.IsPressed();
        playerMovement.putBoost(boostValue);

        bool driftValue = driftAction.IsPressed();
        playerMovement.putDrift(driftValue);
    }

    public void OnPause()
    {
        if (onPauseEvent != null)
        {
            onPauseEvent(this, EventArgs.Empty);
        }
    }
}
