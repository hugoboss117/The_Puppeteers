using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnim : MonoBehaviour
{
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playsound()
    {
        aud.Play();
    }
}
