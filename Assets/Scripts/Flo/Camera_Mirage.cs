using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mirage : MonoBehaviour
{
    public Camera _cam;
    public LayerMask _mirage;

    private Animator _anim;

    void Start()
    {
        _cam.cullingMask &= ~_mirage;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            //SwitchLayers();
            _anim.SetBool("Cam", !_anim.GetBool("Cam"));

    }

    private void SwitchLayers()
    {
        _cam.cullingMask ^= _mirage;
    }
}
