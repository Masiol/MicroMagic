using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySoundPlay : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float delay;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        Invoke("PlayAudio", delay);
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }

}
