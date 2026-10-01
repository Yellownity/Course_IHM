using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerMovement playerMovement;


    float left_right_input;
    float forward;

    InputAction rightLeftAction;
    InputAction forwardAction;

    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        rightLeftAction = playerInput.actions.FindAction("Player/Right_Left");
        forwardAction = playerInput.actions.FindAction("Player/Forward");

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float turnValue = rightLeftAction.ReadValue<float>();
        playerMovement.putTurn(turnValue);

        float forwardValue = forwardAction.ReadValue<float>();
        playerMovement.putForward(forwardValue);
    }
}
