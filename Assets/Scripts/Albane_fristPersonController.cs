using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Albane_PlayerMotor))]
public class Albane_fristPersonController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float lookSensitivityX = 3f;
    [SerializeField]
    private float lookSensitivityY = 3f;
   

    Animator animator;


    private Albane_PlayerMotor motor;
    private Rewind rewind;
    private bool rewinding = false;

    private void Start()
    {
        motor = GetComponent<Albane_PlayerMotor>();
        animator = GetComponent<Animator>();
        rewind = GetComponent<Rewind>();

    }

    private void Update()
    {
        //Mouvement

        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");
        //animator.Play("Walk");

       


        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVectical = transform.forward * _zMov;

        Vector3 _velocity = (_moveHorizontal + _moveVectical).normalized * speed;

        motor.Move(_velocity);

        // Rotate
        float _yRot = Input.GetAxis("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivityX;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxis("Mouse Y");

        Vector3 _camerarotation = new Vector3(_xRot, 0, 0) * lookSensitivityY;

        motor.RotateCamera(_camerarotation);

       /* if (Input.GetKeyDown(KeyCode.A))
        {
           
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {

        }


        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        else if (Input.GetKeyUp(KeyCode.P))
        {

        }*/
    }

    public void Rewind()
    {

    }
}
