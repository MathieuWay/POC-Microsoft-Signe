using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            if (Input.GetKeyDown(KeyCode.E))
                Use();

    }

    public void Use()
    {
        int numPhoto = Photo.UIPhoto.Instance().FindPhoto(key);

        if (numPhoto > -1)
        {
            Photo.UIPhoto.Instance().RemovePhoto(numPhoto);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
