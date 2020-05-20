using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CS_playerMovement : MonoBehaviour
{
    //private CharacterController player;
    private Rigidbody rbPlayer;

    private float accelInput;
    private float turnInput;
    private float carSpeed;
    private float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //player = this.GetComponent<CharacterController>();
        rbPlayer = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get values inputted by player
        accelInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        carSpeed = 100;
        turnSpeed = 20;
        

    }

    void FixedUpdate()
    {

        //movement of player vehicle
        VehicleMovement();

    }

    void LateUpdate()
    {

    }

    private void VehicleMovement()
    {
        //move forward / accelerate
        if (accelInput != 0)
        {
            rbPlayer.velocity = transform.forward * carSpeed * accelInput;
        }


        //turn left and right
        if (turnSpeed !=0)
        {
            this.transform.Rotate(new Vector3(0, turnInput, 0) * turnSpeed * Time.deltaTime);
        }
    }

    private void VehicleAnimations()
    {

    }
}
