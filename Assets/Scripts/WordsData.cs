using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordDatabase", menuName = "Words")]
public class WordsData : ScriptableObject
{
    public GameObject[] usable;
    public List<string> words;

    private void Awake()
    {
        words = new List<string>();
    }

    private void OnEnable()
    {
        usable = GameObject.FindGameObjectsWithTag("Usable");

        if (words.Count > usable.Length)
            while (words.Count > usable.Length)
                words.RemoveAt(words.Count - 1);

        if (words.Count < usable.Length)
            while (words.Count < usable.Length)
                words.Add("");
    }

    public string FindWord(string itemName)
    {
        for (int i = 0; i < usable.Length; i++)
            if(usable[i].name == itemName && words[i] != "")
                return words[i];

        return "";
    }

}
