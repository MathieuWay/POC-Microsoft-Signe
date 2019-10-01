using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Camera_Mirage : MonoBehaviour
{
    public Camera _cam;
    public LayerMask _mirage;

    private Animator _anim;
    private PostProcessLayer _postProcLayer;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _postProcLayer = _cam.GetComponent<PostProcessLayer>();

        _postProcLayer.enabled = false;
        _cam.cullingMask &= ~_mirage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _anim.SetBool("Cam", !_anim.GetBool("Cam"));

    }

    private void SwitchLayers()
    {
        _cam.cullingMask ^= _mirage;
        _postProcLayer.enabled = !_cam.GetComponent<PostProcessLayer>().enabled;
    }
}
