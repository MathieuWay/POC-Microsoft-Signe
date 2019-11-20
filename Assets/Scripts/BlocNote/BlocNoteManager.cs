using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using TMPro;

public class BlocNoteManager : MonoBehaviour
{
    public static BlocNoteManager instance;

    public GameObject sentencePrefab;
    public GameObject wordPrefab;
    public GameObject holePrefab;

    public Transform words;
    public Transform sentences;

    public float interligne;

    public float screenRescaleCoef;

    private Transform blocNote;

    private GameObject instanGO;
    private int[] indexChar;
    private int indexOffset;

    void Start()
    {
        instance = this;

        blocNote = transform.GetChild(0);

        screenRescaleCoef = GetComponent<CanvasScaler>().referenceResolution.x / Screen.width;
        Debug.LogWarning("UI Scale coef : " + screenRescaleCoef);

        AddWord("Oui");
        AddWord("Phrase");
        AddWord("Test");
        AddWord("Bananes");
        AddWord("Consectetur");
        AddWord("quis");
        AddWord("elit");
        AddWord("Hdzqksldq");
        AddWord("42");

        AddSentence("Géneralement, les gens sont déstabilisés lorsqu'une _phrase_ ne se termine pas comme il le patate");
        AddSentence("Lorem ipsum dolor sit amet, _consectetur_ adipiscing _elit_. " +
            "Proin placerat placerat lectus, at porta tortor hendrerit sed. _Mauris_ id nunc euismod, " +
            "placerat turpis _quis_, elementum diam. Sed blandit turpis nisl, laoreet _sodales_ diam dapibus sit amet. " +
            "_Nunc_ tempor augue erat, vitae dapibus metus mattis ultrices. _42_");
    }

    public void AddWord(string mot)
    {
        instanGO = Instantiate(wordPrefab, words);
        instanGO.name = mot;
        instanGO.GetComponent<Text>().text = mot;

        if (words.childCount > 1)
            instanGO.transform.localPosition = new Vector2(
                words.GetChild(words.childCount - 2).localPosition.x,
                words.GetChild(words.childCount - 2).localPosition.y - words.GetChild(words.childCount - 2).GetComponent<RectTransform>().rect.height * (interligne + 1));

        instanGO.GetComponent<RectTransform>().sizeDelta = VecAbs(GetCharPos(instanGO.GetComponent<Text>(), mot + "_", mot.Length, 2)) + new Vector2(1, 1);
        instanGO.GetComponent<BoxCollider2D>().size = instanGO.GetComponent<RectTransform>().sizeDelta;
        instanGO.GetComponent<BoxCollider2D>().offset = new Vector2(instanGO.GetComponent<BoxCollider2D>().size.x / 2f, -instanGO.GetComponent<BoxCollider2D>().size.y / 2f);
    }

    public void AddSentence(string phrase)
    {
        instanGO = Instantiate(sentencePrefab, sentences);
        instanGO.name = phrase.Substring(0, 10);
        //instanGO.GetComponent<Sentences>().phrase = phrase;

        if (sentences.childCount > 1)
            instanGO.transform.localPosition = new Vector2(
                sentences.GetChild(sentences.childCount - 2).localPosition.x,
                sentences.GetChild(sentences.childCount - 2).localPosition.y - sentences.GetChild(sentences.childCount - 2).GetComponent<RectTransform>().rect.height * (interligne + 1));

        indexChar = FindChar(phrase, '_').ToArray();
        

        if (indexChar.Length > 0)
        {
            instanGO.GetComponent<Text>().text = "";
            instanGO.GetComponent<Text>().text += phrase.Substring(0, indexChar[0]);

            Vector3 tempVector;
            GameObject temp;
            indexOffset = 0;

            for (int i = 0; i < indexChar.Length - 2; i += 2)
            {

                temp = Instantiate(holePrefab, instanGO.transform);
                tempVector = GetCharPos(instanGO.GetComponent<Text>(), instanGO.GetComponent<Text>().text + "\'", indexChar[i] + indexOffset, 0);
                //Debug.Log(tempVector);

                instanGO.GetComponent<Text>().text += "_____";
                instanGO.GetComponent<Text>().text += phrase.Substring(indexChar[i + 1] + 1, indexChar[i + 2] - indexChar[i + 1] - 1);

                temp.GetComponent<Hole>().wordIndex = indexChar[i] + indexOffset;

                indexOffset -= indexChar[i + 1] - indexChar[i] + 1;
                indexOffset += 5;

                temp.transform.localPosition += tempVector;
                temp.name = phrase.Substring(indexChar[i] + 1, indexChar[i + 1] - indexChar[i] - 1);
                Debug.Log(indexChar[i] + " " + indexOffset);
            }

            temp = Instantiate(holePrefab, instanGO.transform);
            tempVector = GetCharPos(instanGO.GetComponent<Text>(), instanGO.GetComponent<Text>().text + "\'", indexChar[indexChar.Length - 2] + indexOffset, 0);
            //Debug.Log(tempVector);

            instanGO.GetComponent<Text>().text += "_____";
            instanGO.GetComponent<Text>().text += phrase.Substring(indexChar[indexChar.Length - 1] + 1, phrase.Length - indexChar[indexChar.Length - 1] - 1);

            temp.transform.localPosition += tempVector;
            temp.name = phrase.Substring(indexChar[indexChar.Length - 2] + 1, indexChar[indexChar.Length - 1] - indexChar[indexChar.Length - 2] - 1);
            temp.GetComponent<Hole>().wordIndex = indexChar[indexChar.Length - 2] + indexOffset;
        } else
            instanGO.GetComponent<Text>().text = phrase;


        instanGO.GetComponent<RectTransform>().sizeDelta = new Vector2(
            instanGO.GetComponent<RectTransform>().sizeDelta.x, 
            Mathf.Abs(GetCharPos(instanGO.GetComponent<Text>(), phrase + "_", phrase.Length, 3).y));
    }

    public void FillHole(GameObject sentenceObject, int charIndex, string word)
    {
        string sentence = sentenceObject.GetComponent<Text>().text;
        Debug.Log(sentence.Length + " . " + charIndex + " . " + word.Length + " // " + (sentence.Length - (charIndex + 5 + 5) + charIndex));
        sentenceObject.GetComponent<Text>().text = sentence.Substring(0, charIndex) + word + sentence.Substring(charIndex + 5, sentence.Length - (charIndex + 5));
    }

    private List<int> FindChar(string sentence, char carac)
    {
        List<int> indexCarac = new List<int>();

        for (int i = 0; i < sentence.Length; i++)
            if (sentence[i] == carac)
                indexCarac.Add(i);

        return indexCarac;

    }

    private Vector3 GetCharPos(Text textComp, string phrase, int charIndex, int points)
    {
        string text = phrase;

        //Debug.Log(charIndex + " // " + text);

        if (charIndex >= text.Length)
            return new Vector3();

        TextGenerator textGen = new TextGenerator(text.Length);

        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        TextGenerationSettings textSettings = textComp.GetGenerationSettings(extents);

        textGen.Populate(text, textSettings);

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = ((charIndex - whiteSpace) * 4) + (newLine * 4);

        if (indexOfTextQuad < textGen.vertexCount)
        {
            Vector3 avgPos = textGen.verts[indexOfTextQuad + points].position - textGen.verts[0].position;
            //Vector3 avgPos = new Vector3(textGen.verts[indexOfTextQuad + points].position.x - textGen.verts[0].position.x,
            //    -textGen.verts[indexOfTextQuad + points].position.y + textGen.verts[0].position.y,
            //    textGen.verts[indexOfTextQuad + points].position.z - textGen.verts[0].position.z);

            //Debug.DrawLine(textGen.verts[3].position, textGen.verts[indexOfTextQuad + points].position, Color.blue, 100f);

            //for (int i = 0; i < textGen.verts.Count - 1; i++)
            //    Debug.DrawLine(textGen.verts[i].position, textGen.verts[i+1].position, Color.magenta, 100f);

            //Debug.Log(avgPos);
            return avgPos * screenRescaleCoef;
        } else
        {
            Debug.LogError("Out of text bound");
            return new Vector3();
        }
    }

    private Vector2 VecAbs(Vector2 vec)
    {
        return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
    }
}
