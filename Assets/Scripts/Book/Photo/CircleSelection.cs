using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSelection : MonoBehaviour
{
    public List<Sprite> circleSelection;

    private Image image;

    public void ChangeCircle()
    {
        if (image == null)
            image = GetComponent<Image>();
        image.sprite = circleSelection[Random.Range(0, circleSelection.Count)];
    }
}
