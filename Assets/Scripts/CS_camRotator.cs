using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine;

public class CS_camRotator : MonoBehaviour
{
    public Transform playerToFollow;
    private Vector3 targetPosition;

    private float rotateY = 0f;
    private float rotateX =0f ;
    private Vector3 dRot;
    private Vector3 dPos;
    private bool defaultRotation = true;
    private float posZ = 0;
    
    public Camera firstCamera;
    public Camera secondCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        dRot = new Vector3(16.655f, rotateY, 0f);
        dPos = new Vector3(0f , 0.5586784f , -8.525719f);
        transform.eulerAngles = dRot;
        rotateX = dRot.x;
        posZ = dPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        RotateCameraRotator();
    }

    void FollowPlayer()
    {
        targetPosition = playerToFollow.position;

        transform.position = targetPosition; //Vector3.Lerp(transform.position,
                            // targetPosition, followSpeed * Time.deltaTime);
    }

    void RotateCameraRotator()
    {
        if (Input.GetButtonDown("ResetCameraView"))
        {
            //reset to default values
            defaultRotation = true;
            rotateX = dRot.x;
            rotateY = 0;
            posZ = dPos.z;
        }

        if (Input.GetAxis("CameraLookY") != 0 || Input.GetAxis("CameraLookX") != 0)
        {
            defaultRotation = false;
            rotateX += 2 * Input.GetAxis("CameraLookY") * Time.deltaTime;
            rotateY += 2 * Input.GetAxis("CameraLookX") * Time.deltaTime;
        }

        if (Input.GetAxis("CameraZoom") != 0)
        {
            //print("Zoom In and Out");
            defaultRotation = false;
            posZ += 2 * Input.GetAxis("CameraZoom");
        }


        if (defaultRotation)
        {
            transform.eulerAngles = dRot;
            secondCamera.transform.localPosition = dPos;
            firstCamera.enabled = true;
            secondCamera.enabled = false;
        }

        else
        {
            transform.eulerAngles = new Vector3(rotateX, rotateY, 0f);
            firstCamera.transform.localPosition = new Vector3(0f, dPos.y, posZ);
            firstCamera.enabled = false;
            secondCamera.enabled = true;
            secondCamera.enabled = true;
        }
        

    }

}
