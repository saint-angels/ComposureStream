using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using OscJack;
using UnityEngine;

public class OSCCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras = null;
    
    
    private OscServer server;
    
    // Start is called before the first frame update
    void Start()
    {
        server = new OscServer(9000);
        
        server.MessageDispatcher.AddCallback(
            "/camera_index", // OSC address
            (string address, OscDataHandle data) =>
            {
                var cameraIndex = data.GetElementAsInt(0);
                UnityMainThreadDispatcher.Instance().Enqueue(() => SwitchCameras(cameraIndex));
            }
        );
        
        //Channel indeces are from 1 to 8
        for (int i = 1; i < 8; i++)
        {
            int adressIndex = i;
            server.MessageDispatcher.AddCallback(
                $"/col{adressIndex}", // OSC address
                (string address, OscDataHandle data) =>
                {
                    bool isTurnedOn = data.GetElementAsInt(0) == 1;
        
                    print($"OSC Camera{adressIndex} - {isTurnedOn}");
        
                    int cameraIndex = adressIndex - 1;
                    UnityMainThreadDispatcher.Instance().Enqueue(() => TurnCameraActive(isTurnedOn, cameraIndex));
                }
            );   
        }
        
        

        //ATEM
        // //0 is black. Camera indeces are from 1 to 8
        // for (int i = 1; i < 8; i++)
        // {
        //     int adressIndex = i;
        //     server.MessageDispatcher.AddCallback(
        //         $"/atem/program/{adressIndex}", // OSC address
        //         (string address, OscDataHandle data) =>
        //         {
        //             bool isTurnedOn = data.GetElementAsInt(0) == 1;
        //
        //             print($"OSC Camera{adressIndex} - {isTurnedOn}");
        //
        //             int cameraIndex = adressIndex - 1;
        //             UnityMainThreadDispatcher.Instance().Enqueue(() => TurnCameraActive(isTurnedOn, cameraIndex));
        //         }
        //     );   
        // }
    }
    
    private void TurnCameraActive(bool isActive, int cameraIndex)
    {
        if (cameraIndex < cameras.Length)
        {
            cameras[cameraIndex].gameObject.SetActive(isActive);     
        }
        else
        {
            // Debug.LogError($"Recieved camera index {cameraIndex}, but only {cameras.Length} available!");
        }
    }

    private void SwitchCameras(int cameraIndex)
    {
        if (cameraIndex < cameras.Length)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                bool isCamActive = i == cameraIndex;
                cameras[i].gameObject.SetActive(isCamActive);
            }        
        }
        else
        {
            Debug.LogError($"Recieved camera index {cameraIndex}, but only {cameras.Length} available!");
        }
    }
    
    private void OnDestroy()
    {
        server?.Dispose();
    }
}
