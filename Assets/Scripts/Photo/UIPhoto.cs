﻿using System.Collections;
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
        public LayerMask layerUI;
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
        private LayerMask layerTemp;
        private Vector3 mouseDragOrigin;

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
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if(ui.gameObject.activeSelf)
                    VisualItemToggle(currentIndex);
            }

            if (VisualItemIsVisible())
                VisualItemRotation();
        }

        public void ToggleUI()
        {
            ui.gameObject.SetActive(!ui.gameObject.activeSelf);
            //Camera.main.cullingMask ^= layerUI;

            if (ui.gameObject.activeSelf)
            {
                layerTemp = Camera.main.cullingMask;
                Camera.main.cullingMask = layerUI;
                Time.timeScale = 0;
            } else
            {
                Camera.main.cullingMask = layerTemp;
                Time.timeScale = 1;
            }

            if (VisualItemIsVisible())
                VisualItemToggle(0);
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

            if (VisualItemIsVisible())
                VisualItemToggle(0);

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
            else if (screenshots[index].listObjects.Length > 0)
            {
                itemVisual = Instantiate(screenshots[index].listObjects[0], visualParent.transform);
                //itemVisual.transform.localScale = new Vector3(500, 500, 500); // Probl : A ameliorer (scale non relatif)
                itemVisual.transform.localPosition = new Vector3(0, 0, -300);
                itemVisual.transform.localScale = new Vector3(itemVisual.transform.localScale.x * 600, itemVisual.transform.localScale.y * 600, itemVisual.transform.localScale.z * 600);
                itemVisual.layer = LayerMask.NameToLayer("UI");
                foreach (Transform child in itemVisual.transform)
                    child.gameObject.layer = LayerMask.NameToLayer("UI");
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

            if (Input.GetMouseButtonDown(0))
                mouseDragOrigin = Input.mousePosition;
            else if (Input.GetMouseButton(0))
            {
                visualParent.transform.GetChild(0).Rotate((Input.mousePosition.y - mouseDragOrigin.y) / 10, (mouseDragOrigin.x - Input.mousePosition.x) / 10, 0, Space.World);
                mouseDragOrigin = Input.mousePosition;
            }
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