using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key;
    public Animator anim;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            if (Input.GetKeyDown(KeyCode.E))
                Use();

    }

    public void Use()
    {
        //int numPhoto = Photo.UIPhoto.Instance().FindPhoto(key);

        //if (numPhoto > -1)
        //{
        //    Photo.UIPhoto.Instance().RemovePhoto(numPhoto);
        //    anim.SetBool("isOpen",true);
        //    AudioManager.PlaySound("door");

        //}
        if (BookManager.instance.UIPhoto.EquippedPhotoName() == key.name)
        {
            Photo.UIPhoto.Instance().RemovePhoto(BookManager.instance.UIPhoto.usePhoto);
            anim.SetBool("isOpen", true);
            AudioManager.PlaySound("door");
        }
    }
}
