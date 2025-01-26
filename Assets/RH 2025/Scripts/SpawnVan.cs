using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVan : MonoBehaviour
{
    [SerializeField]
    private LayerMask hitLayerMask = ~0; // Use ~0 for all layers
    [SerializeField]
    private float raycastDistance = 10f; // Adjust the distance as needed
    [SerializeField]
    private GameObject prefabToSpawn; // Assign the prefab to spawn in the Unity Editor
    [SerializeField]
    private GameObject previewPrefab; // Assign the preview mesh in the Unity Editor
    [SerializeField]
    private GameObject raycastOriginObject; // Assign in the Unity Editor

    private GameObject previewInstance; // Instance of the preview mesh
    private GameObject lastSpawnedPrefab; // Instance of the last spawned prefab

    private bool hasSpawned = false; // Flag to track whether the prefab has been spawned

    void Update()
    {
        if (raycastOriginObject == null || prefabToSpawn == null || previewPrefab == null)
        {
            Debug.LogError("Ensure raycastOriginObject, prefabToSpawn, and previewPrefab are assigned.");
            return;
        }

        RaycastHit hit;
        Vector3 raycastOrigin = raycastOriginObject.transform.position;
        Debug.DrawRay(raycastOrigin, raycastOriginObject.transform.forward * raycastDistance, Color.green, 0.1f);

        if (Physics.Raycast(raycastOrigin, raycastOriginObject.transform.forward, out hit, raycastDistance, hitLayerMask))
        {
            if (hit.collider != null)
            {
                Debug.Log("Hit detected on GameObject: " + hit.collider.gameObject.name);
                Debug.Log("Hit point: " + hit.point);
                Debug.Log("Hit normal: " + hit.normal);

                if (previewPrefab != null)
                {
                    if (previewInstance == null)
                    {
                        previewInstance = Instantiate(previewPrefab, hit.point, Quaternion.identity);
                    }
                    else
                    {
                        previewInstance.transform.position = hit.point;
                    }
                }
            }
        }
        else
        {
            Destroy(previewInstance);
        }

        // Rotate the preview prefab using the thumbstick input
        float rotateInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x; // Get horizontal thumbstick input
        if (previewInstance != null)
        {
            previewInstance.transform.Rotate(Vector3.up, rotateInput * Time.deltaTime * 100); // Adjust rotation speed as needed
        }
        // Rotate the preview prefab using the thumbstick input
        float rotateInput2 = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y; // Get horizontal thumbstick input
        if (previewInstance != null)
        {
            previewInstance.transform.Rotate(Vector3.down, rotateInput2 * Time.deltaTime * 100); // Adjust rotation speed as needed
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && previewInstance != null && !hasSpawned)
        {
            if (lastSpawnedPrefab != null)
            {
                Destroy(lastSpawnedPrefab);
            }

            lastSpawnedPrefab = Instantiate(prefabToSpawn, previewInstance.transform.position, previewInstance.transform.rotation);
            Destroy(previewInstance);
            hasSpawned = true;
        }

        if (hasSpawned && previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
    }
}
