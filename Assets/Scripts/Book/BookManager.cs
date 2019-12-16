using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BookManager : MonoBehaviour
{
    // Singleton
    public static BookManager instance;

    public Transform bin;

    public float screenRescaleCoef;
    public LayerMask layerUI;

    // Cahier
    public GameObject book;
    public bool isActive;

    public bool hasCamera;

    // Scripts
    public BlocNoteManager blocNote;
    public Photo.UIPhoto UIPhoto;
    public OptionsManager options;

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
        options = GetComponent<OptionsManager>();

        // Coefficient du rescale de l'UI

        isActive = book.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ToggleBook();
    }

    public void ToggleBook() {
        isActive = !isActive;

        book.SetActive(isActive);


        if (Camera_Mirage.Instance().GetCamState())
            Camera_Mirage.Instance().TogglePostProc();

        Cursor.visible = isActive;

        if (isActive)
        {
            layerTemp = Camera.main.cullingMask;
            Camera.main.cullingMask = layerUI;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Camera.main.cullingMask = layerTemp;
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
        }

        if (UIPhoto.isUIDisplayed())
        {
            UIPhoto.LoadPhotos(UIPhoto.GetCurrentPage());
            UIPhoto.ChangePage(0);
        }
    }

    public void ChangePage(int page)
    {
        UIPhoto.DeselectPhoto();
        UIPhoto.VisualItemOff();
        UIPhoto.VerifyButtons();

        switch (page)
        {
            case 0: // Photos
                UIPhoto.SetActive(true);
                blocNote.SetActive(false);
                options.SetActive(false);
                //UIPhoto.LoadPhotos();
                break;
            case 1: // BlocNote
                blocNote.SetActive(true);
                UIPhoto.SetActive(false);
                options.SetActive(false);
                break;
            case 2:
                options.SetActive(true);
                blocNote.SetActive(false);
                UIPhoto.SetActive(false);
                break;
            default:
                Debug.LogError("Case non-asigned");
                break;
        }
    }

    public void Bin(GameObject obj)
    {
        obj.transform.SetParent(bin);
        Destroy(obj);
    }

}
