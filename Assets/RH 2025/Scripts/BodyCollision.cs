using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject blockingPlane;
    public float maxDistance = 2f;
    public float fadeStartDistance = 1f;

    private Material planeMaterial;
    private Color originalColor;
    private Collider[] triggerColliders; // Array to hold all child colliders

    private void Start()
    {
        // Find all colliders that are children of this GameObject
        triggerColliders = GetComponentsInChildren<Collider>();

        // Get the material of the blocking plane
        planeMaterial = blockingPlane.GetComponent<Renderer>().material;

        // Store the original color and set the transparency to 0
        originalColor = planeMaterial.color;
        originalColor.a = 0f;
        planeMaterial.color = originalColor;
    }

    private void Update()
    {
        float closestDistance = Mathf.Infinity;

        // Find the closest collider
        foreach (var collider in triggerColliders)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, collider.ClosestPoint(mainCamera.transform.position));
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        // Calculate transparency based on the closest collider
        float transparency = Mathf.Clamp01((closestDistance - fadeStartDistance) / (maxDistance - fadeStartDistance));
        Color updatedColor = originalColor;
        updatedColor.a = transparency;
        planeMaterial.color = updatedColor;
    }
}
