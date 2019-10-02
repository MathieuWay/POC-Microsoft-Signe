using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace photo
{
    //private List<structObjects> screenshots = new List<structObjects>();
    //private GameObject ui;
    //public GameObject textNoPhoto;
    //public Image imageDisplayed;
    //public GameObject previousButton;
    //public GameObject nextButton;
    //private int currentIndex = 0;
    ////Singleton
    //private static UIPhoto instance = null;
    //public Texture2D render;
    //public static UIPhoto Instance()
    //{
    //    public GameObject[] listObjects;

    public struct StructObjects
    {
        public GameObject[] listObjects;
        public Texture2D render;

        public StructObjects(List<GameObject> photos, Texture2D texture)
        {
            listObjects = new GameObject[photos.Count];
            photos.CopyTo(listObjects, 0);
            render = texture;
        }
    }

    public class UIPhoto : MonoBehaviour
    {
        private List<StructObjects> screenshots = new List<StructObjects>();
        private GameObject ui;
        public GameObject textNoPhoto;
        public Image imageDisplayed;
        private int currentIndex = 0;

        public List<bool> objectPresent;


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

        private void Awake()
        {
            objectPresent = new List<bool>();
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

        public void NewPhoto(StructObjects obj)
        {
            //screenshots.Add(photo);
            screenshots.Add(obj);
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
        Texture2D tex = screenshots[index].render;
        imageDisplayed.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        if (screenshots.Count > 1)
        {
            previousButton.SetActive(true);
            nextButton.SetActive(true);
            if (index == 0 || index == screenshots.Count - 1)
            {
                if (index == 0)
                    //TODO disable Left button
                    previousButton.SetActive(false);
                else
                    //TODO enable Left button
                    nextButton.SetActive(false);
            }
        }
        else
        {
            previousButton.SetActive(false);
            nextButton.SetActive(false);
        }
    }

        public void ChangeFrame(int direction)
        {
            if (currentIndex + direction >= 0 && currentIndex + direction < screenshots.Count)
            {
                currentIndex += direction;
                LoadIndexToImageDisplayed(currentIndex);
            }
        }
    }
}