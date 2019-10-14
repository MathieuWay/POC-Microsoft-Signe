using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Photo
{
    #region Structures
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
    #endregion

    public class UIPhoto : MonoBehaviour
    {
        public GameObject textNoPhoto;
        public Image imageDisplayed;
        public GameObject previousButton;
        public GameObject nextButton;
        public bool cameraActive;
        public GameObject visualParent;

        private GameObject ui;
        private int currentIndex = 0;
        private List<StructObjects> screenshots = new List<StructObjects>();
        private GameObject itemVisual;

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


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                ToggleUI();
            else if (Input.GetKeyDown(KeyCode.N))
            {
                if(!ui.gameObject.activeSelf)
                    ToggleUI();
                VisualItemToggle(currentIndex);
            }
            if (VisualItemIsVisible())
                VisualItemRotation();
        }

        public void ToggleUI()
        {
            ui.gameObject.SetActive(!ui.gameObject.activeSelf);
            if(ui.gameObject.activeSelf)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        public void NewPhoto(List<GameObject> list, Texture2D photo)
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
                if (VisualItemIsVisible())
                    VisualItemToggle(currentIndex);
            }
        }

        #region Visuel de l'item dans la galerie

        public void VisualItemToggle(int index)
        {
            if (VisualItemIsVisible())
                for (int i = 0; i < visualParent.transform.childCount; i++)
                    Destroy(visualParent.transform.GetChild(i).gameObject);
            else
            {
                itemVisual = Instantiate(screenshots[index].listObjects[0], visualParent.transform);
                itemVisual.transform.localScale = new Vector3(500, 500, 500); // Probl : A ameliorer (scale non relatif)
            }
        }

        private void VisualItemRotation()
        {
            if (Input.GetKey(KeyCode.M))
                visualParent.transform.GetChild(0).Rotate(0, 5, 0);
            if (Input.GetKey(KeyCode.K))
                visualParent.transform.GetChild(0).Rotate(0, -5, 0);
            if (Input.GetKey(KeyCode.O))
                visualParent.transform.GetChild(0).Rotate(5, 0, 0);
            if (Input.GetKey(KeyCode.L))
                visualParent.transform.GetChild(0).Rotate(-5, 0, 0);
        }

        private bool VisualItemIsVisible()
        {
            if (visualParent.transform.childCount > 0)
                return true;

            return false;
        }

        #endregion

        #region  Utilitaires photos

        public bool HasPhoto(GameObject item)
        {
            for (int i = 0; i < screenshots.Count; i++)
                for (int j = 0; j < screenshots[i].listObjects.Length; j++)
                    if(screenshots[i].listObjects[j] == item)
                        return true;

            return false;
        }

        public int FindPhoto(GameObject item)
        {
            for (int i = 0; i < screenshots.Count; i++)
                for (int j = 0; j < screenshots[i].listObjects.Length; j++)
                    if (screenshots[i].listObjects[j] == item)
                        return i;

            return -1;
        }

        public void RemovePhoto(int i)
        {
            screenshots.RemoveAt(i);
            if (currentIndex == i)
                currentIndex--;

            UpdateUI();
        }

        #endregion
    }
}