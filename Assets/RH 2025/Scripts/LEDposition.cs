using System.Collections;
using System.Collections.Generic;
using Sngty;
using UnityEngine;

public class LEDposition : MonoBehaviour
{
    public SingularityManager singularityManager; // Reference to the SingularityManager
    public string messageToSend; // The message to send when the trigger is activated

    void OnTriggerEnter(Collider other)
    {
        // Optional: Add condition to check the triggering object
        if (other.CompareTag("Player")) // Assuming the object has a tag "Player"
        {
            if (singularityManager != null)
            {
                singularityManager.sendMessage(messageToSend); // Call the method in SingularityManager
                Debug.Log($"Message sent on trigger: {messageToSend}");
            }
            else
            {
                Debug.LogError("SingularityManager reference is missing!");
            }
        }
    }
}
