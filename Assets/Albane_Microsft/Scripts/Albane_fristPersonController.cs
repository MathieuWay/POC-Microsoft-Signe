using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albane_fristPersonController : MonoBehaviour
{

    CharacterController Player;
    private Camera eyes;
    public float walkspeed;
    public float sensitiity;
    public float gravity;
    public Vector3 movement;

    float moveFB;
    float moveLR;
    float rotX;
    float rotY;
    float verticalVelosity;

    // Start is called before the first frame update
    void Start()
    {
        eyes = Camera.main;
        Player = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveFB = Input.GetAxis("Vertical") * walkspeed;
        moveLR = Input.GetAxis("Horizontal") * walkspeed;

        rotX = Input.GetAxis("Mouse X") * sensitiity;
        rotY = Input.GetAxis("Mouse Y") * sensitiity;

        rotY = Mathf.Clamp(rotY, -75f, 75f);

        movement = new Vector3(moveLR, verticalVelosity, moveFB);
        transform.Rotate(0, rotX, 0);
        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        movement = transform.rotation * movement;
        Player.Move(movement * Time.deltaTime);

        if (Player.isGrounded)
        {
            verticalVelosity = 0;
        }
        else
        {
            verticalVelosity += gravity * Time.deltaTime;
        }
    }
}
