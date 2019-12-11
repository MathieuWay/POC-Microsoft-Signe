using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogTrigger : MonoBehaviour
{
    public GameObject mirageUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mirageUI.SetActive(true);
            AudioManager.PlaySound("freeze");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mirageUI.SetActive(false);
        }
    }
}
