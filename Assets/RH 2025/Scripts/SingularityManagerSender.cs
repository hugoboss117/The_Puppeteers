using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Net.Sockets;
using System.Text;
using Sngty;
using System;

public class SingularityManagerSender : MonoBehaviour
{
    [System.Serializable]
    public class MessageProtocol
    {
        public string message; // The specific message to send
        public UnityEvent onMessageSent; // UnityEvent to trigger additional actions in the editor
    }

    [Header("Message Protocols")]
    public List<MessageProtocol> messageProtocols = new List<MessageProtocol>();

    [Header("ESP Connection Settings")]
    public string espIPAddress = "192.168.4.1"; // Default ESP IP
    public int espPort = 80; // Default ESP Port

    // [SerializeField] private SingularityManager singularityManager;

    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        // Attempt to connect to ESP on start
        ConnectToESP();
    }

    

    void OnApplicationQuit()
    {
        // Clean up connection when Unity quits
        DisconnectFromESP();
    }

    private void ConnectToESP()
    {
        try
        {
            client = new TcpClient(espIPAddress, espPort);
            stream = client.GetStream();
            Debug.Log("Connected to ESP successfully!");
        }
        catch (SocketException ex)
        {
            Debug.LogError("Failed to connect to ESP: " + ex.Message);
        }
    }

    private void DisconnectFromESP()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }
    }

    public void SendMessageToESP(string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogWarning("ESP is not connected. Attempting to reconnect...");
            ConnectToESP();
        }

        if (stream != null && client.Connected)
        {
            try
            {
                byte[] messageBytes = Encoding.ASCII.GetBytes(message + "\n");
                stream.Write(messageBytes, 0, messageBytes.Length);
                stream.Flush(); // Force the data to be sent immediately
                Debug.Log($"Message sent to ESP: {message}");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error sending message: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to send message. ESP is not connected.");
        }
    }

    public void TriggerMessage(int index)
    {
        if (index >= 0 && index <messageProtocols.Count)
        {
            var protocol = messageProtocols[index];
            if (!string.IsNullOrEmpty(protocol.message))
            {
                SendMessageToESP(protocol.message);
                protocol.onMessageSent?.Invoke();
            }
        }
        else
        
            Debug.LogWarning($"Invalid message protocol index. Count is {messageProtocols.Count}, index is {index}");
        }
    }

