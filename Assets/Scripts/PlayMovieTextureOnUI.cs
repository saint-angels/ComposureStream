using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMovieTextureOnUI : MonoBehaviour 
{
    public RawImage rawimage;

    public Renderer artistRenderer;
    
    void Start () 
    {
        WebCamDevice[] cam_devices = WebCamTexture.devices;
        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < cam_devices.Length; i++) 
        {
            print ("Webcam available: " + cam_devices [i].name);
        }
        
        WebCamTexture webCamTexture = new WebCamTexture(cam_devices[0].name, 1920, 1080, 30);
        
        
        artistRenderer.material.SetTexture("_BaseColorMap", webCamTexture);
        

        // WebCamTexture webcamTexture = new WebCamTexture();
        // rawimage.texture = webcamTexture;
        // rawimage.material.mainTexture = webcamTexture;
        webCamTexture.Play();
    }
}
