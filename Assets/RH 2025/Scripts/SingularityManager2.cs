using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingularityManager2 : MonoBehaviour
{
    [System.Serializable]
    public class MessageProtocol
    {
        public string message; // The specific message string
        public UnityEvent onMessageReceived; // UnityEvent to define actions in the editor
    }

    [Header("Message Protocols")]
    public List<MessageProtocol> messageProtocols = new List<MessageProtocol>();

    // This method will be called by the message system
    public void OnMessageReceived(string message)
    {
        Debug.Log("Message received: " + message);

        // Iterate through the message protocols to find a matching message
        foreach (var protocol in messageProtocols)
        {
            if (protocol.message == message)
            {
                Debug.Log($"Executing actions for message: {message}");
                protocol.onMessageReceived?.Invoke();
                return;
            }
        }

        Debug.LogWarning($"No actions defined for message: {message}");
    }
}
