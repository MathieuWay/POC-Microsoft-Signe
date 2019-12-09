using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Photo
{
    #region Structures
    public struct photo
    {
        public GameObject[] listObjects;
        //public Texture2D render;
        public Sprite sprite;

        //public photo(List<GameObject> photos, Texture2D texture)
        //{
        //    listObjects = new GameObject[photos.Count];
        //    photos.CopyTo(listObjects, 0);
        //    render = texture;
        //}

        public photo(List<GameObject> photos, Texture2D texture)
        {
            listObjects = new GameObject[photos.Count];
            photos.CopyTo(listObjects, 0);
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
    #endregion

    public class UIPhoto : MonoBehaviour
    {
        public Transform ui;
        public LayerMask layerUI;
        //public GameObject textNoPhoto;
        //public Image imageDisplayed;
        public Button previousButton;
        public Button nextButton;
        public Text leftPageNum;
        public Text rightPageNum;
        public Button useButton;
        public Button inspectButton;
        public Button deleteButton;

        public bool cameraActive;
        public GameObject visualParent;

        public List<Transform> page;
        public Transform selection;

        private GameObject[] photos;

        private int photoNumberInPage;

        //public bool hasCamera;

        public int currentIndex = -1;
        private int currentPage = 0;

        private List<photo> screenshots = new List<photo>();
        private GameObject itemVisual;
        private LayerMask layerTemp;

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

        void Start()
        {
            photoNumberInPage = 0;

            for (int i = 0; i < page.Count; i++)
                photoNumberInPage += page[i].childCount;

            photos = new GameObject[photoNumberInPage];

            // Charge les photo dans l'array "photos"
            int j = 0;
            int g = 0;

            for (int i = 0; i < photos.Length; i++)
            {
                photos[i] = page[j].GetChild(g).gameObject;

                g++;
                if (g >= page[j].childCount)
                {
                    j++;
                    g = 0;
                }
            }

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N) && ui.gameObject.activeSelf)
                VisualItemToggle(currentIndex);

            if (VisualItemIsVisible())
                VisualItemRotation();
        }

        public void SetActive(bool state)
        {
            if (ui.gameObject.activeSelf != state)
                ui.gameObject.SetActive(state);

            if (state == true)
                LoadPhotos(currentPage);
        }

        public void ToggleUI()
        {
            if (Camera_Mirage.Instance().GetCamState())
            {
                Camera_Mirage.Instance().TogglePostProc();
            }

            ui.gameObject.SetActive(!ui.gameObject.activeSelf);
            //Camera.main.cullingMask ^= layerUI;
            if (ui.gameObject.activeSelf)
            {
                layerTemp = Camera.main.cullingMask;
                Camera.main.cullingMask = layerUI;
                Time.timeScale = 0;
            }
            else
            {
                Camera.main.cullingMask = layerTemp;
                Time.timeScale = 1;
            }

            if (VisualItemIsVisible())
                VisualItemToggle(0);
        }

        public void NewPhoto(List<GameObject> list, Texture2D photo)
        {
            screenshots.Add(new photo(list, photo));
        }

        //public void DeleteCurrentPhoto()
        //{
        //    if (screenshots.Count > 0)
        //    {
        //        screenshots.RemoveAt(currentIndex);
        //        if (currentIndex > 0)
        //        {
        //            currentIndex--;
        //        }

        //        if (VisualItemIsVisible())
        //            VisualItemToggle(0);

        //        LoadPhotos(currentPage);
        //    }
        //}

        //private void LoadIndexToImageDisplayed(int index)
        //{
        //    Texture2D tex = screenshots[index].render;
        //    imageDisplayed.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        //    if (screenshots.Count > 1)
        //    {
        //        previousButton.SetActive(true);
        //        nextButton.SetActive(true);
        //        if (index == 0 || index == screenshots.Count - 1)
        //        {
        //            if (index == 0)
        //                //TODO disable Left button
        //                previousButton.SetActive(false);
        //            else
        //                //TODO enable Left button
        //                nextButton.SetActive(false);
        //        }
        //    }
        //    else
        //    {
        //        previousButton.SetActive(false);
        //        nextButton.SetActive(false);
        //    }
        //}

        //public void ChangeFrame(int direction)
        //{
        //    if (currentIndex + direction >= 0 && currentIndex + direction < screenshots.Count)
        //    {
        //        currentIndex += direction;
        //        LoadIndexToImageDisplayed(currentIndex);
        //        if (VisualItemIsVisible())
        //            VisualItemToggle(currentIndex);
        //    }
        //}

        public void LoadPhotos(int page)
        {
            for (int i = 0; i < photoNumberInPage; i++)
            {
                if (i + photoNumberInPage * page < screenshots.Count)
                {
                    photos[i].GetComponent<Image>().sprite = screenshots[i + photoNumberInPage * page].sprite;
                    photos[i].GetComponent<Button>().interactable = true;
                } else
                {
                    photos[i].GetComponent<Image>().sprite = null;
                    photos[i].GetComponent<Button>().interactable = false;
                }
            }

            VerifyArrows();
            VerifyButtons();
        }

        public void ChangePage(int page)
        {
            if (currentPage + page >= 0)
                currentPage += page;
            else
                currentPage = 0;

            leftPageNum.text = (currentPage * 2 + 1).ToString();
            rightPageNum.text = (currentPage * 2 + 2).ToString();

            DeselectPhoto();
            LoadPhotos(currentPage);
        }

        public void VerifyArrows()
        {
            if (currentPage > 0)
                previousButton.interactable = true;
            else
                previousButton.interactable = false;

            if (Mathf.Floor((screenshots.Count - 1) / (float)photoNumberInPage) > currentPage)
                nextButton.interactable = true;
            else
                nextButton.interactable = false;
        }

        public void VerifyButtons()
        {
            if (indexInPage(currentIndex))
            {
                useButton.interactable = true;
                inspectButton.interactable = true;
                deleteButton.interactable = true;

                useButton.transform.GetChild(0).gameObject.SetActive(true);
                inspectButton.transform.GetChild(0).gameObject.SetActive(true);
                deleteButton.transform.GetChild(0).gameObject.SetActive(true);
            } else
            {
                useButton.interactable = false;
                inspectButton.interactable = false;
                deleteButton.interactable = false;

                useButton.transform.GetChild(0).gameObject.SetActive(false);
                inspectButton.transform.GetChild(0).gameObject.SetActive(false);
                deleteButton.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        public void SelectPhoto(int index)
        {
            currentIndex = index + photoNumberInPage * currentPage;
            selection.position = photos[index].transform.position;
            selection.GetComponent<CircleSelection>().ChangeCircle();
            selection.gameObject.SetActive(true);
            VerifyButtons();
        }

        public void DeselectPhoto()
        {
            currentIndex = -1;
            selection.gameObject.SetActive(false);
        }

        public void DeleteSelectedPhoto()
        {
            screenshots.RemoveAt(currentIndex);
            DeselectPhoto();
            LoadPhotos(currentPage);
        }

        public void VisualItemSelectedPhoto()
        {
            VisualItemToggle(currentIndex);
        }

        private bool indexInPage(int index)
        {
            if (Mathf.Floor(index / (float)photoNumberInPage) == currentPage)
                return true;

            return false;
        }

        #region Visuel de l'item dans la galerie

        public void VisualItemToggle(int index)
        {
            if (VisualItemIsVisible())
                VisualItemOff();
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
        }

        public void VisualItemOff()
        {
            for (int i = 0; i < visualParent.transform.childCount; i++)
                Destroy(visualParent.transform.GetChild(i).gameObject);
        }

        private bool VisualItemIsVisible()
        {
            if (visualParent.transform.childCount > 0)
                return true;

            return false;
        }

        #endregion

        #region  Utilitaires photos

        public bool isUIDisplayed()
        {
            return ui.gameObject.activeSelf;
        }

        public int GetCurrentPage()
        {
            return currentPage;
        }

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

            LoadPhotos(currentPage);
        }

        #endregion
    }
}