using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashToggle : MonoBehaviour
{

    public GameObject lowerShutter, upperShutter;
    public bool flashActivated=true;

    private void Update()
    {
        Debug.Log("Enriqué");
    }
    public void ToggleFlash()
    {
        Debug.Log("Martin Pêcheur");
        if (flashActivated == true)
        {
            lowerShutter.SetActive(false);
            upperShutter.SetActive(false);
            flashActivated = false;
            Debug.Log("Ours");
        }

        if (flashActivated == false)
        {
            lowerShutter.SetActive(true);
            upperShutter.SetActive(true);
            flashActivated = true;
            Debug.Log("Saumon");
        }
    }

}