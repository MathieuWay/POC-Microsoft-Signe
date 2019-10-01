using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPhoto : MonoBehaviour
{
    private List<Texture> screenshots = new List<Texture>();
    private GameObject ui;
    private static UIPhoto instance = null;
    public static UIPhoto Instance()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType<UIPhoto>();
        if (instance == null)
            Debug.LogError("No UIPhoto in the scene");
        return instance;
    }
    private void Start()
    {
        ui = transform.GetChild(0).gameObject;
    }
    public void ToogleUI()
    {
        ui.gameObject.SetActive(!ui.gameObject.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIPhoto.Instance().ToogleUI();
        }
    }

    public void NewPhoto(Texture2D photo)
    {
        screenshots.Add(photo);
    }
}
