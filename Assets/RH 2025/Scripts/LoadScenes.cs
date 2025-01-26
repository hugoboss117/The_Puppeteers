using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenes : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform[] targets; // Array of target locations
    

    

    public void TeleportToLocation(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < targets.Length)
        {
            Transform target = targets[targetIndex];
            Vector3 offset = head.position - origin.position;
            offset.y = 0; // Adjust for horizontal plane
            origin.position = target.position - offset;

            Vector3 targetForward = target.forward.normalized;
            targetForward.y = 0; // Orientation change horizontal
            Vector3 cameraForward = head.forward.normalized;
            cameraForward.y = 0;

            // Calculate and apply rotation
            float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
            origin.RotateAround(head.position, Vector3.up, angle);

            Debug.Log("Teleported to location: " + targetIndex);
        }
        else
        {
            Debug.LogError("Invalid target index: " + targetIndex);
        }
    }

   
}
