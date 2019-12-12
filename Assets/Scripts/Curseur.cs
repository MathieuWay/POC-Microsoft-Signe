using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curseur : MonoBehaviour
{
    public GameObject curseur;
    public GameObject hand;

    private Transform _selection;
    [SerializeField] private string usableTag = "Usable";
    [SerializeField] private string objectTag = "Camera";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_selection == null)
        {
            hand.SetActive(false);
            curseur.SetActive(false);
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(usableTag))
            {
                curseur.SetActive(true);
            }

            if (selection.CompareTag(objectTag))
            {
                hand.SetActive(true);
            }


        }
    }
}
