using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item
{
    protected override void ApplyItem()
    {
        Debug.Log("Tu a changé les piles de ta lampe torche");
    }
}
