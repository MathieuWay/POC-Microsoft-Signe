using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Photo
{
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
        public GameObject previousButton;
        public GameObject nextButton;
        private int currentIndex = 0;
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

        public void NewPhoto(List<GameObject> list,Texture2D photo)
        {
            screenshots.Add(new StructObjects(list, photo));
            currentIndex = screenshots.Count - 1;
            UpdateUI();
        }

        public void DeleteCurrentPhoto()
        {
            screenshots.RemoveAt(currentIndex);
            if (currentIndex > 0)
            {
                currentIndex--;
            }
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