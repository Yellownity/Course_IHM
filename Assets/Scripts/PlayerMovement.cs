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
    [SerializeField] float maxSpeed;
    [SerializeField] float orthogonalReduction;
    private float forward;

    private float turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turn(turn);
        ReduceOrthogonalVelocity();
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
        this.transform.Rotate(transform.up, input*speedTurn*Time.fixedDeltaTime);

        //float getpadright = Mathf.Sign((gamepad.rightStick.ReadValue().x));
        //Vector3 move = transform.rotation*(new Vector3(0, 0, getpadright)) * 20 ;
        //this.rigidbody.AddForce(move);
    }
    private void Move()
    {
        if (this.rigidbody.linearVelocity.magnitude > maxSpeed)
        {
            return;
        }
        float speed = forward > 0 ? speedForward : speedBackward;
        this.rigidbody.AddForce(this.transform.forward * forward * speed *10,ForceMode.Acceleration);

    }
    private void ReduceOrthogonalVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(transform.forward, this.rigidbody.linearVelocity);
        Vector3 orthogonalVelocity = transform.right * Vector3.Dot(transform.right, this.rigidbody.linearVelocity);

        this.rigidbody.linearVelocity = forwardVelocity + orthogonalVelocity * orthogonalReduction;
    }

}
