using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentences : MonoBehaviour
{
    public string phrase;

    private Text text;
    private int wordIndex;

    private string[] holeFull;

    private int[] indexChar;

    private void Start()
    {
        text = GetComponent<Text>();
        holeFull = new string[transform.childCount];
        Debug.LogError("//");
    }

    //public void FillWord(string mot)
    //{
    //    wordIndex = FindWord(mot);

    //    if (wordIndex > -1)
    //    {
    //        Debug.Log(text.text.Substring(0, wordIndex) + mot);
    //        text.text = text.text.Substring(0, wordIndex) + mot + text.text.Substring(wordIndex + 5, phrase.Length - wordIndex + 5);
    //    }
    //}

    public void FillHole(int holeIndex, string mot)
    {
        holeFull[holeIndex] = mot;

        CalculateSentence();
        VerifySentence();
    }

    public void EmptyHole(int holeIndex)
    {
        holeFull[holeIndex] = null;

        CalculateSentence();
    }

    private void CalculateSentence()
    {
        indexChar = BlocNoteManager.instance.FindChar(phrase, '_').ToArray();

        if (indexChar.Length > 0)
        {
            gameObject.GetComponent<Text>().text = "";
            gameObject.GetComponent<Text>().text += phrase.Substring(0, indexChar[0]);

            Vector3 tempVector;
            int indexOffset = 0;
            GameObject temp = transform.GetChild(0).gameObject;

            for (int i = 0; i < indexChar.Length - 2; i += 2)
            {
                temp = transform.GetChild(i / 2).gameObject;
                tempVector = BlocNoteManager.instance.GetCharPos(gameObject.GetComponent<Text>(), gameObject.GetComponent<Text>().text + "\'", gameObject.GetComponent<Text>().text.Length, 0);

                temp.transform.localPosition = new Vector2(-temp.transform.parent.GetComponent<RectTransform>().rect.width / 2, 0);
                temp.transform.localPosition += tempVector;

                if (holeFull[i / 2] == null)
                {
                    //temp = Instantiate(holePrefab, gameObject.transform);
                    //Debug.Log(tempVector);
                    gameObject.GetComponent<Text>().text += "_____";
                    gameObject.GetComponent<Text>().text += phrase.Substring(indexChar[i + 1] + 1, indexChar[i + 2] - indexChar[i + 1] - 1);
                    //temp.GetComponent<Hole>().wordIndex = indexChar[i] + indexOffset;
                    indexOffset -= indexChar[i + 1] - indexChar[i] + 1;
                    indexOffset += 5;
                    //temp.name = phrase.Substring(indexChar[i] + 1, indexChar[i + 1] - indexChar[i] - 1);
                }
                else
                {
                    gameObject.GetComponent<Text>().text += holeFull[i / 2] + phrase.Substring(indexChar[i + 1] + 1, indexChar[i + 2] - indexChar[i + 1] - 1);
                }
            }

            temp = transform.GetChild(indexChar.Length / 2 - 1).gameObject;
            tempVector = BlocNoteManager.instance.GetCharPos(gameObject.GetComponent<Text>(), gameObject.GetComponent<Text>().text + "\'", gameObject.GetComponent<Text>().text.Length, 0);

            temp.transform.localPosition = new Vector2(-temp.transform.parent.GetComponent<RectTransform>().rect.width / 2, 0);
            temp.transform.localPosition += tempVector;

            if (holeFull[indexChar.Length / 2 - 1] == null)
            {
                //Debug.Log(tempVector);
                gameObject.GetComponent<Text>().text += "_____";
                gameObject.GetComponent<Text>().text += phrase.Substring(indexChar[indexChar.Length - 1] + 1, phrase.Length - indexChar[indexChar.Length - 1] - 1);
                //temp.GetComponent<Hole>().wordIndex = indexChar[indexChar.Length - 2] + indexOffset;
                //temp.name = phrase.Substring(indexChar[indexChar.Length - 2] + 1, indexChar[indexChar.Length - 1] - indexChar[indexChar.Length - 2] - 1);
            }
            else
            {
                gameObject.GetComponent<Text>().text += holeFull[holeFull.Length - 1] + phrase.Substring(indexChar[indexChar.Length - 1] + 1, phrase.Length - indexChar[indexChar.Length - 1] - 1);
            }
        }
        else
            gameObject.GetComponent<Text>().text = phrase;

        BlocNoteManager.instance.PlaceWords();
    }

    private void VerifySentence()
    {
        for (int i = 0; i < holeFull.Length; i++)
            if (holeFull[i] == null)
                return;

        for (int i = 0; i < holeFull.Length; i++)
            if (holeFull[i].ToLower() != transform.GetChild(i).name.ToLower())
            {
                BlocNoteManager.instance.ResetWords();
                return;
            }

        Debug.LogError("WIIIIIIIN !!!");
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
