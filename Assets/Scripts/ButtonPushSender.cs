using System;
using System.Collections;
using System.Collections.Generic;
using OscJack;
using UnityEngine;

public class ButtonPushSender : MonoBehaviour
{
    private OscClient client;
    
    void Awake()
    {
        client = new OscClient("127.0.0.1", 9000);
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SendIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SendIndex(1);
        }
    }
    
    private void SendIndex(int index)
    {
        client.Send("/camera_index", index);
    }

    private void OnDestroy()
    {
        client?.Dispose();
    }
}
