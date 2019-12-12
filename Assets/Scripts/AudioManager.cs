using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioClip freezeSound, shutterSound, buttonSound, doorSound, stepSound, bookSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        freezeSound = Resources.Load<AudioClip>("freezeSound");
        shutterSound = Resources.Load<AudioClip>("shutterSound");
        buttonSound = Resources.Load<AudioClip>("buttonSound");
        doorSound = Resources.Load<AudioClip>("doorSound");
        stepSound = Resources.Load<AudioClip>("stepSound");
        bookSound = Resources.Load<AudioClip>("bookSound");


        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "freeze":
                audioSrc.PlayOneShot(freezeSound);
                break;
            case "shutter":
                audioSrc.PlayOneShot(shutterSound);
                break;
            case "button":
                audioSrc.PlayOneShot(buttonSound);
                break;
            case "door":
                audioSrc.PlayOneShot(doorSound);
                break;
            case "step":
                audioSrc.PlayOneShot(stepSound);
                break;
            case "book":
                audioSrc.PlayOneShot(bookSound);
                break;
        }
    }
}
