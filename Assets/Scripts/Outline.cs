using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{

    public Material[] material;
    Renderer rend;
    public bool isHighlighted = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
            isHighlighted = true;
        else if (Input.GetMouseButtonUp(1))
            isHighlighted = false;

        if (isHighlighted == true)
        {
            rend.sharedMaterial = material[1];
        }
        if (isHighlighted == false)
        {
            rend.sharedMaterial = material[0];
        }
        
    }
}
