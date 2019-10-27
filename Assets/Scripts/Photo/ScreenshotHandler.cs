using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    public Camera myCamera;
    public float photoDistance;
    public bool takePhotoWithCamera;
    //public Camera eyes;
    private bool takeScreenshotOnNextFrame;
    
    //public RawImage rawimg; Sert à voir en temps réel si le système marche

    private RenderTexture renderTexture;
    private RaycastHit hit;
    
    //public int screenshotCapacity; Le nombre de pellicules disponible dans l'appareil
    //private int screenshotsTaken = 0; Le nombre de pellicules contenant une photo

    public AudioClip cameraShutter;
    public AudioSource sfxSource;
    

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        //eyes.enabled = true;
    }

    void Start()
    {
        sfxSource.clip = cameraShutter;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0 && (Photo.UIPhoto.Instance().cameraActive || !takePhotoWithCamera))
            TakeScreenshot(500, 500);
        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
            //Debug.Log(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 2, out hit, 2));
            //if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 2, out hit, 2) && hit.collider.CompareTag("Usable"))
                //Debug.Log(hit.collider.name);
        //}

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * photoDistance, Color.green);
        if (Input.GetKeyDown(KeyCode.Space)){
        sfxSource.Play();
    }
    }

    

    private void TakeScreenshot(int width, int height)
    {
        //if(screenshotsTaken<screenshotCapacity) {
        
        myCamera.targetTexture = RenderTexture.GetTemporary(myCamera.pixelWidth, myCamera.pixelHeight, 16);
        //Change le rendu de la caméra en une texture temporaire

        takeScreenshotOnNextFrame = true;
        //Lance le signal pour démarrer la fonction OnPostRender

        myCamera.Render();
        //screenshotsTaken++; Prend un espace dans une pellicule

        myCamera.targetTexture=null;
        //Remets le rendu normal de la caméra
        //}
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            //Empêche la procédure de tourner en boucle

            renderTexture = myCamera.targetTexture;
            //Applique la texture de la caméra à une renderTexture indépendante
            
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            renderResult.Apply();
            //Applique la RenderTexture sur une Texture2D

            // Photo avec TOUS les objets visibles (Probleme objets sans mesh renderer)
            //List<GameObject> tempList = new List<GameObject>();
            //GameObject[] goTagArray = GameObject.FindGameObjectsWithTag("Usable");

            //for (int i = 0; i < goTagArray.Length; i++)
            //    if (goTagArray[i].GetComponent<Renderer>().isVisible)
            //        tempList.Add(goTagArray[i]);


            //Photo.UIPhoto.Instance().NewPhoto(tempList, renderResult);

            List<GameObject> tempList = new List<GameObject>();

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, photoDistance) && hit.collider.CompareTag("Usable"))
            {
                tempList.Add(hit.collider.gameObject);

                for (int i = 0; i < tempList.Count; i++)
                    Debug.Log(tempList[i] + " " + i);
            }


            Photo.UIPhoto.Instance().NewPhoto(tempList, renderResult);

            
            //rawimg.texture = renderResult; Sert à tester en direct sur une raw Image


        }
    }
    
    /*public void ShowOverheadView()
    {
        myCamera.enabled = true;
        eyes.enabled = false;
    }*/


}
