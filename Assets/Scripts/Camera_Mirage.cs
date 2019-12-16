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
    private FxPro _postProcLayer;

    private float fieldOfViewIni;

    //Singleton
    private static Camera_Mirage instance = null;

    public static Camera_Mirage Instance()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType<Camera_Mirage>();
        if (instance == null)
            Debug.LogError("No Camera_Mirage in the scene");
        return instance;
    }

    void Start()
    {
        
        _anim = GetComponent<Animator>();
        _postProcLayer = _cam.GetComponent<FxPro>();

        _cam.cullingMask &= ~_mirageLayer;
        _postProcLayer.enabled = false;
        _mirageUI.SetActive(false);

        fieldOfViewIni = Camera.main.fieldOfView;
    }

    void Update()
    {
        //ShowOverheadView();

        if (Input.GetMouseButtonDown(1) && !BookManager.instance.isActive && BookManager.instance.hasCamera)
            ToggleCamera();
    }

    private void SwitchLayers()
    {
        _cam.cullingMask ^= _mirageLayer;
        TogglePostProc();
        _mirageUI.SetActive(!_mirageUI.activeSelf);

        if ((_cam.cullingMask & _mirageLayer) != 0)
            Photo.UIPhoto.Instance().cameraActive = true;
        else
            Photo.UIPhoto.Instance().cameraActive = false;
    }

    public bool GetCamState()
    {
        return _anim.GetBool("Cam");
    }

    public void ToggleCamera()
    {
        _anim.SetBool("Cam", !_anim.GetBool("Cam"));
    }

    public void TogglePostProc()
    {
        _postProcLayer.enabled = !_postProcLayer.enabled;

        if(Camera.main.fieldOfView != fieldOfViewIni)
            Camera.main.fieldOfView = fieldOfViewIni;
        else
            Camera.main.fieldOfView -= 10;
    }
    /*public void ShowOverheadView()
    {
        _cam.enabled = false;
        screenShotCam.enabled = true;
    }*/
}
