using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Dialog", menuName ="Dialog")]
public class Dialog : ScriptableObject
{
    public string charName;
    public string charSpeech;

    public Sprite charFace;
}
