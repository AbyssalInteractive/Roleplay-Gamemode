using UnityEngine;
using Telepathy;
using System;

public class TCPServer : MonoBehaviour
{
    Server server = new Server();

    void Start()
    {
        server.Start(2501);
    }

    void Update()
    {
        Message msg;

        while(server.GetNextMessage(out msg))
        {
            switch(msg.eventType)
            {
                case EventType.Connected:
                    Debug.Log(msg.connectionId + " connected");
                    break;
                case EventType.Data:
                    Debug.Log(msg.connectionId + " Data: " + BitConverter.ToString(msg.data));
                    break;
                case EventType.Disconnected:
                    Debug.Log(msg.connectionId + " disconnected");
                    break;
            }
        }
    }

    void OnDestroy()
    {
        server.Stop();
    }
}