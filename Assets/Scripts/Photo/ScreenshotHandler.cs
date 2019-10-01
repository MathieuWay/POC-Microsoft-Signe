using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    public RawImage rawimg;
    private RenderTexture renderTexture;
    

    public int screenshotCapacity;
    private int screenshotsTaken = 0;
    

    private void Awake(){
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeScreenshot(500,500);
        }
    }

    private void OnPostRender(){
        if (takeScreenshotOnNextFrame){
            takeScreenshotOnNextFrame = false;
            renderTexture = myCamera.targetTexture;
           
            
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);;
            
            renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            renderResult.Apply();

            UIPhoto.Instance().NewPhoto(renderResult);
            //rawimg.texture = renderResult; Sert à tester en direct sur une raw Image
            
            
        }
    }

    private void TakeScreenshot(int width, int height){
        if(screenshotsTaken<screenshotCapacity){
        
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
        myCamera.Render();
        screenshotsTaken++;
        myCamera.targetTexture=null;
        }
    }
    
    
}
