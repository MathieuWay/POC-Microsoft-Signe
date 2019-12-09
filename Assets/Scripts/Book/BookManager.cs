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
    public LayerMask layerUI;

    // Cahier
    public GameObject book;
    public bool isActive;

    public bool hasCamera;

    // Scripts
    private BlocNoteManager blocNote;
    private Photo.UIPhoto UIPhoto;

    private LayerMask layerTemp;

    private void Awake()
    {
        instance = this;

        screenRescaleCoef = GetComponent<CanvasScaler>().referenceResolution.x / Screen.width;
        Debug.LogWarning("UI Scale coef : " + screenRescaleCoef);
    }

    void Start()
    {
        blocNote = GetComponent<BlocNoteManager>();
        UIPhoto = GetComponent<Photo.UIPhoto>();

        // Coefficient du rescale de l'UI

        isActive = book.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            blocNote.ToggleBlocNote();
        else if (Input.GetKeyDown(KeyCode.N))
            UIPhoto.ToggleUI();
        else if (Input.GetKeyDown(KeyCode.A))
            ToggleBook();
    }

    public void ToggleBook() {
        isActive = !isActive;

        book.SetActive(isActive);

        if (Camera_Mirage.Instance().GetCamState())
            Camera_Mirage.Instance().TogglePostProc();

        if (isActive)
        {
            layerTemp = Camera.main.cullingMask;
            Camera.main.cullingMask = layerUI;
            Time.timeScale = 0;
        } else
        {
            Camera.main.cullingMask = layerTemp;
            Time.timeScale = 1;
        }

        if (UIPhoto.isUIDisplayed())
        {
            UIPhoto.LoadPhotos(UIPhoto.GetCurrentPage());
            UIPhoto.ChangePage(0);
        }
    }

    public void ChangePage(int page)
    {
        switch (page)
        {
            case 0: // Photos
                UIPhoto.SetActive(true);
                blocNote.SetActive(false);
                UIPhoto.DeselectPhoto();
                //UIPhoto.LoadPhotos();
                break;
            case 1: // BlocNote
                blocNote.SetActive(true);
                UIPhoto.SetActive(false);
                break;
            default:
                Debug.LogError("Case non-asigned");
                break;
        }
    }

}
