using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private bool takeScreenshotOnNextFrame;
    public List<Texture> screenshots = new List<Texture>();

    private Camera myCamera;

    private void Awake(){
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender(){
        if (takeScreenshotOnNextFrame){
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;
            screenshots.Add(renderTexture);
            Debug.Log("Added Screenshot to the list");
        }
    }

    private void TakeScreenshot(int width, int height){
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height){
        instance.TakeScreenshot(width, height);
    }
}
