using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMoter))]
public class Player_Controler : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float JumpForce = 1000f;

    [Header("jont setings:")]
   
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jontMaxForce = 40f;

    private PlayerMoter motor;
    private ConfigurableJoint joint;

    void Start () //all of this hapens on wake
    {
        motor = GetComponent<PlayerMoter>();
        joint = GetComponent<ConfigurableJoint>();
        SetJontSettings(jointSpring);
    }

    void Update()
    {
        //calculate movement volocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal"); //-1 to 1
        float zMov = Input.GetAxisRaw("Vertical");   //-1 to 1
        //making individual vectors
        Vector3 _movHorizontal = transform.right * xMov; 
        Vector3 _movVertical = transform.forward * zMov;
        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        //apply movment
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

       float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);


        Vector3 _jumpforce = Vector3.zero;
        //apply jump
        if (Input.GetButton ("Jump"))
        {
            _jumpforce = Vector3.up * JumpForce;
            SetJontSettings(0f);
        }else
        {
            SetJontSettings(jointSpring);
        }
        motor.ApplyJump(_jumpforce);
    }

    private void SetJontSettings (float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            
            positionSpring = jointSpring,
            maximumForce = jontMaxForce
        };
    }
}
