using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public class ArduinoControl : MonoBehaviour
{
    public int p1;
    public int p2;
    public int p3;
    public int p4;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arrived:" + msg);

        string[] data = msg.Split(':');
        int number = int.Parse(data[1].Trim());
        print(number + data[0]);
        if(number != 0)
        {
            switch (data[0])
            {
                case "P1":
                    p1 = number;
                    break;
                case "P2":
                    p2 = number;
                    break;
                case "P3":
                    p3 = number;
                    break;
                case "P4":
                    p4 = number;
                    break;
            }
        }
      
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}