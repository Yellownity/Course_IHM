using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float speedForward;
    [SerializeField] float speedBackward;
    [SerializeField] float speedTurn;
    private float forward;

    private float turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn(turn);
        Move();
    }
    public void putTurn(float input)
    {
        turn = input;
    }
    public void putForward(float input)
    {
        forward = input;
    }
    private void Turn(float input)
    {
        this.transform.Rotate(transform.up, input*speedTurn);

        //float getpadright = Mathf.Sign((gamepad.rightStick.ReadValue().x));
        //Vector3 move = transform.rotation*(new Vector3(0, 0, getpadright)) * 20 ;
        //this.rigidbody.AddForce(move);
    }
    private void Move()
    {
        float speed = forward > 0 ? speedForward : speedBackward;
        this.rigidbody.AddForce(this.transform.forward * forward * speed);
    }

}
