﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

//[RequireComponent(typeof(Albane_fristPersonController))]
//[RequireComponent(typeof(Albane_PlayerMotor))]
public class RewindVideo : MonoBehaviour
{
    private VideoPlayer videoplayer;

    public bool paused = true;
    public bool reversed = false;
    public float duration = 0.75f;
    private float startTime = 0;
    public float currentSpeed = 0;
    public float targetSpeed = 0;
    public float reverseSpeedFactor = 2;

    // Start is called before the first frame update
    void Start()
    {
        videoplayer = GetComponent<VideoPlayer>();
        videoplayer.Play();
        Debug.Log(videoplayer.length);
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        float actualSpeed = Mathf.Lerp(currentSpeed, targetSpeed, t);
        //Debug.Log("min:"+currentSpeed+"/max:"+ targetSpeed + "/t:"+t);
        //achived speed
        if(targetSpeed - actualSpeed == 0)
            currentSpeed = targetSpeed;

        if (videoplayer.time + actualSpeed * Time.deltaTime > videoplayer.length)
            videoplayer.time = videoplayer.length;
        else if (videoplayer.time + actualSpeed * Time.deltaTime < 0)
            videoplayer.time = 0;
        else
        {
            if (reversed && videoplayer.playbackSpeed == 0)
            {
                videoplayer.time += actualSpeed * Time.deltaTime * reverseSpeedFactor;
            }
            else
                videoplayer.playbackSpeed = actualSpeed;
        }
    }
    
    public void PlayScene()
    {
        startTime = Time.time;
        targetSpeed = 1;
    }

    public void PauseScene()
    {
        startTime = Time.time;
        targetSpeed = 0;
    }

    public void ToggleScene()
    {
        if (paused)
            PlayScene();
        else
            PauseScene();
        paused = !paused;
    }

    public void ResumeScene()
    {
        reversed = false;
        if (!paused)
        {
            PlayScene();
        }
        else
        {
            if(targetSpeed != 0)
                PauseScene();
        }
    }

    public void ReverseScene()
    {
        startTime = Time.time;
        targetSpeed = -1;
        reversed = true;
    }
}