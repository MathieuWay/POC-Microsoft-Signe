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

        // Graph & button
        public Button previousButton;
        public Button nextButton;
        public Text leftPageNum;
        public Text rightPageNum;
        public Button useButton;
        public Button inspectButton;
        public Button deleteButton;

        public List<Transform> page;

        public Transform selection;

        // Inspect
        public GameObject inspectParent;
        public GameObject visualParent;

        // Use photo
        public int usePhoto = -1;
        public Image equipedPhoto;

        public bool cameraActive;

        // Current
        public int currentIndex = -1;
        private int currentPage = 0;

        private GameObject[] photos; // Array contenant tous les boutons "photos"
        private int photoNumberInPage;

        private List<photo> screenshots = new List<photo>();
        private GameObject itemVisual;
        private LayerMask layerTemp;

        private Vector3 mouseDragOrigin;

        #region Singleton
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
        #endregion

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
                AudioManager.PlaySound("book");
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

        public void LoadPhotos(int page)
        {
            for (int i = 0; i < photoNumberInPage; i++)
            {
                if (i + photoNumberInPage * page < screenshots.Count)
                {
                    if (i + photoNumberInPage * page != usePhoto)
                        photos[i].GetComponent<Button>().interactable = true;
                    else
                        photos[i].GetComponent<Button>().interactable = false;

                    photos[i].GetComponent<Image>().sprite = screenshots[i + photoNumberInPage * page].sprite;
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
                deleteButton.interactable = true;

                useButton.transform.GetChild(0).gameObject.SetActive(true);
                deleteButton.transform.GetChild(0).gameObject.SetActive(true);
                if (screenshots[currentIndex].listObjects.Length > 0)
                {
                    inspectButton.interactable = true;
                    inspectButton.transform.GetChild(0).gameObject.SetActive(true);
                } else {
                    inspectButton.interactable = false;
                    inspectButton.transform.GetChild(0).gameObject.SetActive(false);
                }
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

        #region Fonctions des boutons

        public void DeselectPhoto()
        {
            currentIndex = -1;
            selection.gameObject.SetActive(false);
        }

        public void DeleteSelectedPhoto()
        {
            RemovePhoto(currentIndex);
        }

        public void EquipPhoto()
        {
            usePhoto = currentIndex;

            if (equipedPhoto != null)
            {
                equipedPhoto.sprite = screenshots[usePhoto].sprite;

                equipedPhoto.gameObject.SetActive(true);

                //StartCoroutine(GoTo(equipedPhoto.transform, photos[usePhoto % photoNumberInPage].transform, equipedPhoto.transform));
            }

            DeselectPhoto();
            //photos[usePhoto].GetComponent<Button>().interactable = false;
            LoadPhotos(currentPage);
        }

        public void VisualItemSelectedPhoto()
        {
            VisualItemToggle(currentIndex);
        }

        public void ResetRotationVisual()
        {
            visualParent.transform.GetChild(0).rotation = Quaternion.identity;
        }

        #endregion

        #region Visuel de l'item dans la galerie

        public void VisualItemToggle(int index)
        {
            if (VisualItemIsVisible())
                VisualItemOff();
            else if (screenshots[index].listObjects.Length > 0)
            {
                itemVisual = Instantiate(screenshots[index].listObjects[0], visualParent.transform);
                //itemVisual.transform.localScale = new Vector3(500, 500, 500); // Probl : A ameliorer (scale non relatif)
                itemVisual.transform.localPosition = new Vector3(0, 0, -350);
                itemVisual.transform.localScale = RealScale(screenshots[index].listObjects[0].transform) * 3f;
                itemVisual.transform.rotation = Quaternion.identity;
                //itemVisual.transform.localScale = new Vector3(itemVisual.transform.localScale.x * 600, itemVisual.transform.localScale.y * 600, itemVisual.transform.localScale.z * 600);
                itemVisual.layer = LayerMask.NameToLayer("UI");
                foreach (Transform child in itemVisual.transform)
                    child.gameObject.layer = LayerMask.NameToLayer("UI");

                inspectParent.SetActive(true);
            }
        }

        private void VisualItemRotation()
        {
            if (Input.GetKey(KeyCode.D))
                visualParent.transform.GetChild(0).Rotate(0, 5, 0);
            if (Input.GetKey(KeyCode.Q))
                visualParent.transform.GetChild(0).Rotate(0, -5, 0);
            if (Input.GetKey(KeyCode.Z))
                visualParent.transform.GetChild(0).Rotate(5, 0, 0);
            if (Input.GetKey(KeyCode.S))
                visualParent.transform.GetChild(0).Rotate(-5, 0, 0);

            if (Input.GetMouseButtonDown(0))
                mouseDragOrigin = Input.mousePosition;
            else if (Input.GetMouseButton(0))
            {
                visualParent.transform.GetChild(0).Rotate((Input.mousePosition.y - mouseDragOrigin.y) / 2, (mouseDragOrigin.x - Input.mousePosition.x) / 2, 0, Space.Self);
                mouseDragOrigin = Input.mousePosition;
            }
        }

        public void VisualItemOff()
        {
            for (int i = 0; i < visualParent.transform.childCount; i++)
                Destroy(visualParent.transform.GetChild(i).gameObject);

            inspectParent.SetActive(false);
        }

        private bool VisualItemIsVisible()
        {
            if (visualParent.transform.childCount > 0)
                return true;

            return false;
        }

        private Vector3 RealScale(Transform item)
        {
            Vector3 Return = item.transform.localScale;
            while (item.parent != null)
            {
                //Debug.Log(item.parent.name + " // " + item.parent.localScale.x + " . " + Return.x);
                item = item.parent;
                Return = Vector3.Scale(Return, item.transform.localScale);
            }
            return Return;
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
            if (usePhoto > i)
                usePhoto--;

            screenshots.RemoveAt(i);

            DeselectPhoto();
            LoadPhotos(currentPage);
        }

        private bool indexInPage(int index)
        {
            if (Mathf.Floor(index / (float)photoNumberInPage) == currentPage)
                return true;

            return false;
        }

        #endregion

        IEnumerator GoTo(Transform item, Transform from, Transform to) // Essai, fail, position != position absolue ? Pas le meme canvas /!\
        {
            item.position = from.position;
            Vector3 dir = (item.localPosition - to.localPosition) / 10f;
            Debug.Log(item.name + " . " + from.name + " // " + item.position + " . " + to.position + " // " + dir);
            while (item.localPosition != to.localPosition)
            {
                yield return new WaitForSeconds(1f);
                item.localPosition += dir;
            }
        }
    }
}