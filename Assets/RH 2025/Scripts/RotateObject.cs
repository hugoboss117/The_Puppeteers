using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Public variables to set rotation axis and speed in the editor
    public Vector3 rotationAxis = Vector3.up; // Default to rotating around the Y-axis
    public float rotationSpeed = 10f; // Default speed

    void Update()
    {
        // Rotate the GameObject based on the axis and speed
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
