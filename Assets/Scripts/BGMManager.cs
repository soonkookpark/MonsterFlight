using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance { get; private set; }
    public AudioClip backGrouundSound; // Drag your audio file in the inspector to set this variable.

    private AudioSource audioSource; // This will play the sound.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager instance already exists, destroying this one.");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (audioSource != null && backGrouundSound != null)
        {
            audioSource.PlayOneShot(backGrouundSound);
        }
    }


}
