using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    
    //public RawImage rawimg; Sert à voir en temps réel si le système marche

    private RenderTexture renderTexture;
    

    //public int screenshotCapacity; Le nombre de pellicules disponible dans l'appareil
    //private int screenshotsTaken = 0; Le nombre de pellicules contenant une photo
    

    private void Awake(){
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            TakeScreenshot(500,500);
        }

    }

    private void TakeScreenshot(int width, int height){
        //if(screenshotsTaken<screenshotCapacity){
        
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        //Change le rendu de la caméra en une texture temporaire

        takeScreenshotOnNextFrame = true;
        //Lance le signal pour démarrer la fonction OnPostRender

        myCamera.Render();
        //screenshotsTaken++; Prend un espace dans une pellicule

        myCamera.targetTexture=null;
        //Remets le rendu normal de la caméra
        //}
    }

    private void OnPostRender(){
        if (takeScreenshotOnNextFrame){
            takeScreenshotOnNextFrame = false;
            //Empêche la procédure de tourner en boucle

            renderTexture = myCamera.targetTexture;
            //Applique la texture de la caméra à une renderTexture indépendante
            
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);;
            renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            renderResult.Apply();
            //Applique la RenderTexture sur une Texture2D

            List<GameObject> tempList = new List<GameObject>();
            GameObject[] goTagArray = GameObject.FindGameObjectsWithTag("Usable");

            for (int i = 0; i < goTagArray.Length; i++)
                if (goTagArray[i].GetComponent<Renderer>().isVisible)
                    tempList.Add(goTagArray[i]);
                

            Photo.UIPhoto.Instance().NewPhoto(tempList, renderResult);
            //rawimg.texture = renderResult; Sert à tester en direct sur une raw Image
            
            
        }
    }
    
    
}
