using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerMovement playerMovement;


    float left_right_input;
    float forward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Left_Right(InputAction.CallbackContext context)
    {
        left_right_input = context.ReadValue<float>();
        playerMovement.putTurn(left_right_input);
    }

    public void Forward(InputAction.CallbackContext context)
    {
        forward = context.ReadValue <float>();
        playerMovement.putForward(forward);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
