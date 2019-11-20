using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public List<Tutorial> Tutorials = new List<Tutorial>();
    private Tutorial currentTutorial;
    public Text explanationText;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();
            if (instance == null)
                Debug.Log("No tutorial found");
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (!currentTutorial)
        {
            //return;
            CompletedAllTutorials();
            explanationText.text = "";
            Debug.Log("Tutorial fini");
        }
        else
        {
            if (currentTutorial.explanation != "")
                explanationText.text = currentTutorial.explanation;
        }
    }

    public void CompletedAllTutorials()
    {
        explanationText.text = "";
        //LevelManager.Instance.SetTutorialFinished();
        instance = null;
        transform.gameObject.SetActive(false);
    }

    public Tutorial GetTutorialByOrder(int Order)
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].order == Order)
                return Tutorials[i];
        }
        return null;
    }

    public int GetCurrentOrder()
    {
        return currentTutorial.GetOrder();
    }
}