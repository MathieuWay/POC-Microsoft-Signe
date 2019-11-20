using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentences : MonoBehaviour
{
    public string phrase;

    private Text text;
    private int wordIndex;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void FillWord(string mot)
    {
        wordIndex = FindWord(mot);

        if (wordIndex > -1)
        {
            Debug.Log(text.text.Substring(0, wordIndex) + mot);
            text.text = text.text.Substring(0, wordIndex) + mot + text.text.Substring(wordIndex + 5, phrase.Length - wordIndex + 5);
        }
    }

    private int FindWord(string mot)
    {
        for (int i = 0; i < phrase.Length; i++)
            if (phrase[i] == mot[0] && phrase.Length - i > mot.Length)
                for (int j = 1; j < mot.Length; j++)
                    if (phrase[i + j] == mot[j])
                        return i;

        return -1;
    }
}
