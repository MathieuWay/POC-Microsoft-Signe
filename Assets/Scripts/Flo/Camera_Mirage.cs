using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class Camera_Mirage : MonoBehaviour
{
    public Camera _cam;
    //public Camera screenShotCam;
    public LayerMask _mirageLayer;
    public GameObject _mirageUI;

    private Animator _anim;
    private PostProcessLayer _postProcLayer;
    

    void Start()
    {
        
        _anim = GetComponent<Animator>();
        _postProcLayer = _cam.GetComponent<PostProcessLayer>();

        _cam.cullingMask &= ~_mirageLayer;
        _postProcLayer.enabled = false;
        _mirageUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _anim.SetBool("Cam", !_anim.GetBool("Cam"));
        //ShowOverheadView();
        
    }

    private void SwitchLayers()
    {
        _cam.cullingMask ^= _mirageLayer;
        _postProcLayer.enabled = !_cam.GetComponent<PostProcessLayer>().enabled;
        _mirageUI.SetActive(!_mirageUI.activeSelf);
    }

    /*public void ShowOverheadView()
    {
        _cam.enabled = false;
        screenShotCam.enabled = true;
    }*/
}
