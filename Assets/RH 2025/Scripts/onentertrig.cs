using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onentertrig : MonoBehaviour
{
    [Header("Scene to load on trigger enter")]
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the scene name is set and load it
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned!");
        }
    }
}
