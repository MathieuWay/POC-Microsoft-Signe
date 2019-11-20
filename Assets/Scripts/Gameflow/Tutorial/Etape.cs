using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Etape : Tutorial
{
    public Transform hitTransform;
    private bool playerInside = false;

    public override void CheckIfHappening()
    {
        if(playerInside)
            TutorialManager.Instance.CompletedTutorial();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform == hitTransform)
        {
            playerInside = true;
        }
        else
        {
            playerInside = false;
        }
    }
}