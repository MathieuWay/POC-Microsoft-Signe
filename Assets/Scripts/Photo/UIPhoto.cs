using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPhoto : MonoBehaviour
{
    private List<structObjects> screenshots = new List<structObjects>();
    private GameObject ui;
    public GameObject textNoPhoto;
    public Image imageDisplayed;
    private int currentIndex = 0;
    private struct structObjects
    {
        List<GameObject> listObjects = new List<GameObject>();
        Texture2D render;
    }
    //Singleton
    private static UIPhoto instance = null;
    public static UIPhoto Instance()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType<UIPhoto>();
        if (instance == null)
            Debug.LogError("No UIPhoto in the scene");
        return instance;
    }
    //

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
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Texture2D texture = new Texture2D(960, 640);
            // colors used to tint the first 3 mip levels
            Color[] colors = new Color[3];
            colors[0] = Color.red;
            colors[1] = Color.green;
            colors[2] = Color.blue;
            int mipCount = Mathf.Min(3, texture.mipmapCount);

            // tint each mip level
            for (int mip = 0; mip < mipCount; ++mip)
            {
                Color[] cols = texture.GetPixels(mip);
                for (int i = 0; i < cols.Length; ++i)
                {
                    cols[i] = Random.ColorHSV();
                }
                texture.SetPixels(cols, mip);
            }
            // actually apply all SetPixels, don't recalculate mip levels
            texture.Apply(false);
            NewPhoto(texture);
        }*/
    }

    public void NewPhoto(Texture2D photo)
    {
        screenshots.Add(photo);
        currentIndex = screenshots.Count - 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (screenshots.Count > 0)
        {
            LoadIndexToImageDisplayed(currentIndex);
            imageDisplayed.gameObject.SetActive(true);
            textNoPhoto.SetActive(false);
        }
        else
        {
            textNoPhoto.SetActive(true);
            imageDisplayed.gameObject.SetActive(false);
        }
    }

    private void LoadIndexToImageDisplayed(int index)
    {
        Texture2D tex = screenshots[index];
        imageDisplayed.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        //if(index == 0)
        //TODO disable Left button
        //else
        //TODO enable Left button
        //if(index == screenshots.Count)
        //TODO disable right button
        //else
        //TODO enable right button
    }

    public void ChangeFrame(int direction)
    {
        if(currentIndex + direction >= 0 && currentIndex + direction < screenshots.Count)
        {
            currentIndex += direction;
            LoadIndexToImageDisplayed(currentIndex);
        }
    }
}
