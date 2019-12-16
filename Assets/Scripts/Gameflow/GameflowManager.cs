using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameflowManager : MonoBehaviour
{

    public List<Step> steps = new List<Step>();
    public Step currentStep;
    public Text explanationText;

    private static GameflowManager instance;
    public static GameflowManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameflowManager>();
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
        if (currentStep)
            currentStep.UpdateGameflow();
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentStep.order + 1);
        if(currentStep != null)
            currentStep.StartGameflow();
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentStep = GetTutorialByOrder(currentOrder);
        if (!currentStep)
        {
            //return;
            CompletedAllTutorials();
            explanationText.text = "";
            Debug.Log("Tutorial fini");
        }
        else
        {
            explanationText.text = currentStep.explanation;
        }
    }

    public void CompletedAllTutorials()
    {
        explanationText.text = "";
        //LevelManager.Instance.SetTutorialFinished();
        instance = null;
        transform.gameObject.SetActive(false);
    }

    public Step GetTutorialByOrder(int Order)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            if (steps[i].order == Order)
                return steps[i];
        }
        return null;
    }

    public int GetCurrentOrder()
    {
        return currentStep.GetOrder();
    }
}