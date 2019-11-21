using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{

    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;
    public GameObject SelectionManager;
    // Start is called before the first frame update
    void Start()
    {
        myText = GameObject.Find("Text").GetComponent<Text>();
        myText.color = Color.clear;
    }

    // Update is called once per frame


    public void ShowText()
    {
        displayInfo = true;
        
    }

    public void HideText()
    {
        displayInfo = false;
        Debug.Log("ours");

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            displayInfo = false;
        }
    }
    void Update()
    {
        FadeText();
        
    }

    void FadeText()
    {
        if (displayInfo)
        {
            myText.text = myString;
            myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
        }
        else
        {
            myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }

}
