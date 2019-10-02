using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            if (Input.GetKeyDown(KeyCode.R))
                Use();

    }

    public void Use()
    {
        if(Photo.UIPhoto.Instance().HasPhoto(key))
            gameObject.SetActive(false);
    }
}
