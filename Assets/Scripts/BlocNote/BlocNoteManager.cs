using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class BlocNoteManager : MonoBehaviour
{
    public Transform debug;

    public GameObject wordPrefab;
    public GameObject holePrefab;

    public Transform words;
    public Transform sentences;

    public float interligne;

    private Transform blocNote;

    private float screenRescaleCoef;

    private GameObject instanGO;
    private int[] indexChar;
    private int indexOffset;

    void Start()
    {
        blocNote = transform.GetChild(0);

        screenRescaleCoef = GetComponent<CanvasScaler>().referenceResolution.x / Screen.width;
        Debug.LogWarning(screenRescaleCoef);

        addWord("Oui");
        addWord("Test");
        addWord("Bananes");
        addWord("42");

        //addSentence("Géneralement, les gens sont déstabilisés lorsqu'une _phrase_ ne se termine pas comme il le patate");
        addSentence("Lorem ipsum dolor sit amet, 'consectetur' adipiscing 'elit'. Proin placerat placerat lectus, at porta tortor hendrerit sed. 'Mauris' id nunc euismod, placerat turpis quis, elementum diam. Sed blandit turpis nisl, laoreet sodales diam dapibus sit amet. Nunc tempor augue erat, vitae dapibus metus mattis ultrices.");
    }

    public void addWord(string mot)
    {
        instanGO = Instantiate(wordPrefab, words);
        instanGO.name = mot;
        instanGO.GetComponent<Text>().text = mot;

        if (words.childCount > 1)
            instanGO.transform.localPosition = new Vector2(
                words.GetChild(words.childCount - 2).localPosition.x,
                words.GetChild(words.childCount - 2).localPosition.y - words.GetChild(words.childCount - 2).GetComponent<RectTransform>().rect.height * (interligne + 1));
    }

    public void addSentence(string phrase)
    {
        indexOffset = 0;
        instanGO = Instantiate(wordPrefab, sentences);
        instanGO.name = phrase.Substring(0, 8);

        indexChar = findChar(phrase, '\'').ToArray();

        

        if (indexChar.Length > 0)
        {
            instanGO.GetComponent<Text>().text = "";
            instanGO.GetComponent<Text>().text += phrase.Substring(0, indexChar[0]);

            Vector3 tempVector;
            GameObject temp;

            //Debug.Log(indexChar.Length);

            for (int i = 0; i < indexChar.Length - 2; i += 2)
            {

                temp = Instantiate(holePrefab, instanGO.transform);
                tempVector = GetCharPos(instanGO.GetComponent<Text>(), indexChar[i] + indexOffset, instanGO.GetComponent<Text>().text + "\'");
                Debug.Log(tempVector);

                instanGO.GetComponent<Text>().text += "_____";
                instanGO.GetComponent<Text>().text += phrase.Substring(indexChar[i + 1] + 1, indexChar[i + 2] - indexChar[i + 1] - 1);

                indexOffset -= indexChar[i + 1] - indexChar[i] + 1;
                indexOffset += 5;

                temp.transform.localPosition += tempVector;
            }



            temp = Instantiate(holePrefab, instanGO.transform);
            tempVector = GetCharPos(instanGO.GetComponent<Text>(), indexChar[indexChar.Length - 2] + indexOffset, instanGO.GetComponent<Text>().text + "\'");
            Debug.Log(tempVector);

            instanGO.GetComponent<Text>().text += "_____";
            instanGO.GetComponent<Text>().text += phrase.Substring(indexChar[indexChar.Length - 1] + 1, phrase.Length - indexChar[indexChar.Length - 1] - 1);

            temp.transform.localPosition += tempVector;
        } else
            instanGO.GetComponent<Text>().text = phrase;
    }

    private List<int> findChar(string sentence, char carac)
    {
        List<int> indexCarac = new List<int>();

        for (int i = 0; i < sentence.Length; i++)
            if (sentence[i] == carac)
                indexCarac.Add(i);

        return indexCarac;

    }

    private Vector3 GetCharPos(Text textComp, int charIndex, string phrase)
    {
        string text = phrase;

        Debug.Log(charIndex + " // " + text);

        if (charIndex >= text.Length)
            return new Vector3();

        TextGenerator textGen = new TextGenerator(text.Length);

        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        TextGenerationSettings textSettings = textComp.GetGenerationSettings(extents);
        //textSettings.fontSize = textSettings.fontSize * 10 / 24;

        textGen.Populate(text, textSettings);

        //Debug.Log(textComp.GetGenerationSettings(extents).fontSize + " // " + extents + " - " + textSettings.fontSize);

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = ((charIndex - whiteSpace) * 4) + (newLine * 4);

        if (indexOfTextQuad < textGen.vertexCount)
        {
            //Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
            //    textGen.verts[indexOfTextQuad + 1].position +
            //    textGen.verts[indexOfTextQuad + 2].position +
            //    textGen.verts[indexOfTextQuad + 3].position) /4f;

            Vector3 avgPos = textGen.verts[indexOfTextQuad].position - textGen.verts[0].position;

            //Debug.Log(textGen.verts[indexOfTextQuad].position + " // " + textGen.verts[0].position + " // " + indexOfTextQuad);
            Debug.DrawLine(debug.position + textGen.verts[3].position, debug.position + textGen.verts[indexOfTextQuad].position, Color.blue, 100f);

            //avgPos = new Vector2(avgPos.x - textGen.verts[0].position.x, avgPos.y - textGen.verts[0].position.y);

            //Debug.Log(indexOfTextQuad);
            //print(avgPos);
            //Debug.Log(avgPos);
            //PrintWorldPos(textComp, avgPos);
            for (int i = 0; i < textGen.verts.Count - 1; i++)
            {
                //Debug.Log(textGen.verts[i].position);
                //PrintWorldPos(textComp, textGen.verts[i].position);
                //PrintWorldPos(textComp, textGen.verts[i].position, textGen.verts[i + 1].position);
                Debug.DrawLine(debug.position + textGen.verts[i].position, debug.position + textGen.verts[i+1].position, Color.magenta, 100f);
            }

            return avgPos * screenRescaleCoef;
        } else
        {
            Debug.LogError("Out of text bound");
            return new Vector3();
        }
    }

    void PrintWorldPos(Text textComp, Vector3 testPoint)
    {
        Vector3 worldPos = textComp.transform.TransformPoint(testPoint);
        print(worldPos);
        new GameObject("point").transform.position = worldPos;
        Debug.DrawRay(worldPos, Vector3.up, Color.red, 50f);
        Debug.DrawRay(worldPos, Vector3.up, Color.red);
    }

    void PrintWorldPos(Text textComp, Vector3 testPoint, Vector3 testPoint2)
    {
        Vector3 worldPos = textComp.transform.TransformPoint(testPoint);
        Vector3 worldPos2 = textComp.transform.TransformPoint(testPoint2);
        //print(worldPos + " " + worldPos2);
        new GameObject("point").transform.position = worldPos;
        new GameObject("point").transform.position = worldPos2;
        Debug.DrawLine(worldPos, worldPos2, Color.green, 100f);
    }
}
