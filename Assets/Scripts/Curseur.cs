using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curseur : MonoBehaviour
{
    public GameObject curseur;
    public GameObject hand;
    public BookManager bookManager;

    private Transform _selection;
    [SerializeField] private string usableTag = "Usable";
    [SerializeField] private string objectTag = "Camera";

    void Update()
    {
        if(_selection == null)
        {
            hand.SetActive(false);
            curseur.SetActive(false);
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && bookManager.isActive == false)
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

        if (bookManager.isActive == true)
        {
            curseur.SetActive(false);
            hand.SetActive(false);
        }
    }
}
