using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CS_playerCamFollow : MonoBehaviour
{
    public Transform playerToFollow;
    public Vector3 cameraOffset;
    public float followSpeed = 10;
    public float lookSpeed = 10;
    private Vector3 lookDirection;
    private Vector3 targetPosition;
    private Quaternion cameraRotation;

    private Vector3 defaultCamOffset;
    private float minZ = -15f;
    private float maxZ = 15f;

    // Start is called before the first frame update
    void Start()
    {
        defaultCamOffset = new Vector3(0, 3, -8);
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraOffset();
    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
        LookAtTarget();
    }

    void LateUpdate()
    {

    }

    void LookAtTarget()
    {
        lookDirection = playerToFollow.position - transform.position;
        cameraRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, 
                             cameraRotation, lookSpeed * Time.deltaTime);
    }

    void MoveTowardsTarget()
    {
        targetPosition = playerToFollow.position +
                         playerToFollow.forward * cameraOffset.z +
                         playerToFollow.right * cameraOffset.x +
                         playerToFollow.up * cameraOffset.y;

        transform.position = Vector3.Lerp(transform.position, 
                             targetPosition, followSpeed * Time.deltaTime);
    }

    void SetCameraOffset()
    {
        
        if (Input.GetAxis("CameraZoom") != 0)
        {
            //print("Zoom In and Out");
            //print(Input.GetAxis("CameraZoom"));
            cameraOffset.z += Input.GetAxis("CameraZoom");

            if (cameraOffset.z > maxZ)
                cameraOffset.z = maxZ;

            else if (cameraOffset.z < minZ)
                cameraOffset.z = minZ;
        }

        if (Input.GetButtonDown("ResetCameraView"))
        {
            //set camera axis to default position
            //print("Reset Camera to Default");
            cameraOffset = defaultCamOffset;
        }
    }
}
