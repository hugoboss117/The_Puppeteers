using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectoff : MonoBehaviour
{
    public GameObject objectToToggle;

    private void Start()
    {
        // Make sure the object is initially turned off
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    public void ToggleObject()
    {
        // Toggle the state of the GameObject when the button is clicked
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }

}
