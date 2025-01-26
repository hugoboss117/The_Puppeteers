using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // The object to rotate around
    public Transform target;

    // The speed of rotation
    public float rotationSpeed = 10.0f;

    // The distance from the target
    public float distance = 5.0f;

    // The axis of rotation
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        if (target != null)
        {
            // Calculate the position of the object to maintain the specified distance
            Vector3 direction = (transform.position - target.position).normalized;
            Vector3 desiredPosition = target.position + direction * distance;

            // Update the position
            transform.position = desiredPosition;

            // Rotate around the target
            transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }
}
