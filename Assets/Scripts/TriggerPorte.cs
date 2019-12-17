using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerPorte : MonoBehaviour
{
    public string[] expected = new string[4] { "bureau", "jouer", "clé", "pot de fleur" };
    public Animator doorAnimator;
    private void Start()
    {
        BlocNoteManager.wordAddedDelegate = testWord;
    }

    private void testWord()
    {
        bool flag = false;
        //List<string> expectedList = new List<string> { "bureau", "jouer", "clé", "pot de fleur" };
        Transform[] transforms = BlocNoteManager.instance.words.GetComponentsInChildren<Transform>();
        List<string> transformsList = new List<string>();
        foreach (Transform word in transforms) transformsList.Add(word.name);

        int i = 0;
        while (i < expected.Length && !flag) {
            if (!transformsList.Contains(expected[i]))
                flag = true;
            i++;
        }
        Debug.Log("Object Trouver = " + (i - 1));
        Debug.Log(string.Join(", ", transformsList.ToArray()));
        if (!flag)
        {
            doorAnimator.SetBool("isOpen", true);
        }
    }
}
