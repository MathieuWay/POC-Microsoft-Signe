using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogDisplay : MonoBehaviour
{

    public Dialog dialog;

    public Text UIName;
    public Text UISpeech;
    
    public Image UIFace;

    // Start is called before the first frame update
    void Start()
    {
        UIName.text = dialog.charName;
        UISpeech.text = dialog.charSpeech;

        UIFace.sprite = dialog.charFace;
    }

}
