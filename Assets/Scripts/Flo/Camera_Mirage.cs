using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mirage : MonoBehaviour
{
    public LayerMask _mirage;

    private Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();

        _cam.cullingMask &= ~_mirage;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            SwitchLayers(_mirage);
    }

    private void SwitchLayers(int layer)
    {
        //if((_cam.cullingMask & layer) > 0)
        //    _cam.cullingMask &= ~layer;
        //else
        //    _cam.cullingMask |= layer;

        _cam.cullingMask ^= layer;
    }
}
