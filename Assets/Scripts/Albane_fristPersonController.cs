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
    [SerializeField]
    public PlayableDirector director;
    private bool paused = false;
    public float duration = 0.75f;
    private float startTime = 0;
    public float currentSpeed = 0;


    private Albane_PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<Albane_PlayerMotor>();
    }

    private void Update()
    {
        //Mouvement

        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

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

        //Stop Timeline 

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (director.state == PlayState.Playing)
            {
                director.Pause();
                paused = true;
            }
            else
            {
                director.Play();
                paused = false;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {

            //Should be on play to reverse
            if (director.state != PlayState.Playing)
                director.Play();
            //Calcule speed
            if (Input.GetKeyDown(KeyCode.A))
                startTime = Time.time;
            // Calculate the fraction of the total duration that has passed.
            float t = (Time.time - startTime) / duration;
            //currentSpeed = Mathf.Lerp(0, 1, currentSpeed + Time.fixedDeltaTime);
            currentSpeed = Mathf.SmoothStep(0, 1, t);
            //add time
            director.time -= currentSpeed * Time.deltaTime;
            if (director.time < 0)
                director.time = 0;
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.A))
                startTime = Time.time;
            if (currentSpeed <=  0f)
            {
                //Debug.Log("reset speed(speed:"+ currentSpeed +")");
                currentSpeed = 0;
                if (director.state != PlayState.Paused && paused)
                {
                    director.Pause();
                }
            }
            else
            {
                float t = (Time.time - startTime) / duration;
                currentSpeed = Mathf.Lerp(1, 0, t);
                director.time -= currentSpeed * Time.deltaTime;
                if (director.time < 0)
                    director.time = 0;
            }
        }
    }
}
