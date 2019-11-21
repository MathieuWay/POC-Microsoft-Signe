using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photo;

public class LidFX : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0 && UIPhoto.Instance().cameraActive && !UIPhoto.Instance().isUIDisplayed()) {
            animator.Play("blink");
        }
        
    }
}
