using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Waypoint : Step
{
    public Transform hitTransform;
    private bool playerInside = false;

    public override void UpdateGameflow()
    {
        if (playerInside)
            GameflowManager.Instance.CompletedTutorial();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform == hitTransform)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == hitTransform)
        {
            playerInside = false;
        }
    }
}