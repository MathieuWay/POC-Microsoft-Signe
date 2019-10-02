using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


//[RequireComponent(typeof(Albane_fristPersonController))]
//[RequireComponent(typeof(Albane_PlayerMotor))]
public class Rewind : MonoBehaviour
{
    
    private PlayableDirector director;

    private bool paused = false;
    public float duration = 0.75f;
    private float startTime = 0;
    public float currentSpeed = 0;


    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKeyUp(KeyCode.A))
                startTime = Time.time;
            if (currentSpeed <= 0f)
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
                currentSpeed = Mathf.SmoothStep(1, 0, t);
                director.time -= currentSpeed * Time.deltaTime;
                if (director.time < 0)
                    director.time = 0;
            }
        }
    }
}
