using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BookManager : MonoBehaviour
{
    // Singleton
    public static BookManager instance;

    public float screenRescaleCoef;

    // Graph du cahier
    public GameObject bookGraph;
    public bool isActive;

    // Scripts
    private BlocNoteManager blocNote;
    private Photo.UIPhoto UIPhoto;

    void Start()
    {
        instance = this;

        blocNote = GetComponent<BlocNoteManager>();
        UIPhoto = GetComponent<Photo.UIPhoto>();

        // Coefficient du rescale de l'UI
        screenRescaleCoef = GetComponent<CanvasScaler>().referenceResolution.x / Screen.width; 
        Debug.LogWarning("UI Scale coef : " + screenRescaleCoef);

        isActive = bookGraph.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            blocNote.ToggleBlocNote();
        else if (Input.GetKeyDown(KeyCode.A))
            UIPhoto.ToggleUI();

    }
}
