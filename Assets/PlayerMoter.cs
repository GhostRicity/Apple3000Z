using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoter : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float curCamRotX = 0f;
    private Vector3 jumpForce = Vector3.zero;
    [SerializeField]
    private float vamLimit = 85f;
    

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    //Get a movment vector
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    
    // Gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cemeraRotationX)
    {
        cameraRotationX = _cemeraRotationX;
    }
    // we apply the jump force
    public void ApplyJump(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
    }

    // runn all physics
    void FixedUpdate()
    {
        PreformMovement();  //makes no sence |I know just wait for it brav
        PreformRotation();
    }
    
    // movment based on velocity
    void PreformMovement ()
    {
        if (velocity !=Vector3.zero)    //why this? Well its is easeyer to control then the add force method simple as that 
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (jumpForce != Vector3.zero)
        {
            rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    //prefoirm rotation
    void PreformRotation ()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // set location and clamp it
            curCamRotX -= cameraRotationX;
            curCamRotX = Mathf.Clamp(curCamRotX, -vamLimit, vamLimit);
            //apply limited rotation to camera
            cam.transform.localEulerAngles = new Vector3(curCamRotX, 0f, 0f);
        }
    }
}
