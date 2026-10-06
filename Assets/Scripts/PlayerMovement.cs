using System;
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
    [SerializeField] float normalOrthogonalReduction;
    [SerializeField] float driftOrthogonalReduction;
    private float forward;

    private float turn;
    private bool isDrifting;
    private ParticleSystem driftParticle;


    private bool isBoosting;
    [SerializeField] private float boostAmount = 100f;
    [SerializeField] private float boostConsumption = 30f;
    [SerializeField] private float boostForce = 30f;

    public static event EventHandler<float> changingBoostUIEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        driftParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turn(turn);
        Move();
        DriftIfNeeded();

    }
    public void putTurn(float input)
    {
        turn = input;
    }
    public void putForward(float input)
    {
        forward = input;
    }

    public void putBoost(bool input)
    {
        isBoosting = input;
    }

    public void putDrift(bool input)
    {
        isDrifting = input;
    }

    private void Turn(float input)
    {
        float minSpeedForTurn = rigidbody.linearVelocity.magnitude / 8;
        minSpeedForTurn = Mathf.Clamp01(minSpeedForTurn);
        this.transform.Rotate(transform.up, input * speedTurn * Time.fixedDeltaTime * minSpeedForTurn);
    }
    private void Move()
    {


        if (isBoosting && boostAmount > 0)
        {
            this.rigidbody.AddForce(
                this.transform.forward * boostForce,
                ForceMode.Acceleration
            );

            boostAmount -= boostConsumption * Time.fixedDeltaTime;
            boostAmount = Mathf.Max(boostAmount, 0f);
            if (changingBoostUIEvent != null)
            {
                changingBoostUIEvent(this, boostAmount);
            }
        }

        if (!isBoosting && boostAmount < 100f)
        {
            boostAmount += (boostConsumption / 2f) * Time.fixedDeltaTime;
            boostAmount = Mathf.Min(boostAmount, 100f);
            if (changingBoostUIEvent != null)
            {
                changingBoostUIEvent(this, boostAmount);
            }
        }
        // on mets une limite de vitesse pour ne pas dépasser la vitesse max après le boost
        if (this.rigidbody.linearVelocity.magnitude > maxSpeed)
        {
            return;
        }

        float speed = forward > 0 ? speedForward : speedBackward;
        this.rigidbody.AddForce(this.transform.forward * forward * speed * 10, ForceMode.Acceleration);

    }

    private void DriftIfNeeded()
    {
        if (!isDrifting)
        {
            ReduceOrthogonalVelocity(normalOrthogonalReduction);
            return;
        }
        ReduceOrthogonalVelocity(driftOrthogonalReduction);
        driftParticle.Emit(10);
    }
    private void ReduceOrthogonalVelocity(float reduction)
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(transform.forward, this.rigidbody.linearVelocity);
        Vector3 orthogonalVelocity = transform.right * Vector3.Dot(transform.right, this.rigidbody.linearVelocity);

        this.rigidbody.linearVelocity = forwardVelocity + orthogonalVelocity * reduction;
    }

}
