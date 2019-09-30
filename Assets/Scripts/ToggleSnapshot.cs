using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSnapshot : MonoBehaviour
{

    public SnapshotCamera snapCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            snapCam.CallTakeSnapshot();
        }
    }
}
