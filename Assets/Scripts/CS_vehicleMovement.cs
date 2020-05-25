using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_vehicleMovement : MonoBehaviour
{
    //instantiate variables that store player input
    private float horizontalInput, verticalInput, steeringAngle;
    public WheelCollider FrontWheelR, FrontWheelL, RearWheelR, RearWheelL;
    //public Transform FWheelR_T, FWheelL_T, RWheelR_T, RWheelL_T;
    public float maxSteerAngle = 30; //in degrees, for steering angle
    public float motorForce = 0;
    public float brakeForce = 1000;

    private float maxMotorForce = 1000;
    private float minMotorForce = 0;
    private float acceleration = 100;

    private bool isbraking = false;

    private Rigidbody rbVehicle;

    // Start is called before the first frame update
    void Start()
    {
        rbVehicle = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        ComputeVehicleSpeed();
    }

    void FixedUpdate()
    {
        SteerVehicle();
        VehicleMove();
        HandleBrake();
    }

    void LateUpdate()
    {

    }

    void GetPlayerInput()
    {
        horizontalInput = Input.GetAxis("Steering");
        verticalInput = Input.GetAxis("Accelerate");
        
    }

    void SteerVehicle()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        FrontWheelL.steerAngle = steeringAngle;
        FrontWheelR.steerAngle = steeringAngle;
    }

    void VehicleMove()
    {
        if (!isbraking)
        {
            FrontWheelR.motorTorque = verticalInput * motorForce;
            FrontWheelL.motorTorque = verticalInput * motorForce;
        }
    }

    void ComputeVehicleSpeed()
    {
        if (Input.GetAxis("Accelerate") != 0 && motorForce < maxMotorForce)
        {
            motorForce += acceleration * Time.deltaTime;
        }

        else if (Input.GetAxis("Accelerate") != 0 && motorForce >= maxMotorForce)
        {
            motorForce = maxMotorForce;
        }

        else
        {
            motorForce -= acceleration * Time.deltaTime;

            if (motorForce <= minMotorForce)
            {
                motorForce = minMotorForce;
            }
        }

    }

    void HandleBrake()
    {
        if (Input.GetAxis("Brake") > 0)
        {
            isbraking = true;
            print("brake pressed");
        }

        else
        {
            isbraking = false;
        }

        if (isbraking)
        {
            FrontWheelR.brakeTorque = brakeForce;
            FrontWheelL.brakeTorque = brakeForce;
            FrontWheelR.motorTorque = 0;
            FrontWheelL.motorTorque = 0;
        }

        else
        {
            FrontWheelR.brakeTorque = 0;
            FrontWheelL.brakeTorque = 0;
        }
    }
}
