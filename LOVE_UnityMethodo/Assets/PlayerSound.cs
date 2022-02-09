using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip deathSound;
    public AudioClip jumpSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    public void PlayerJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayerDies()
    {
        audioSource.PlayOneShot(deathSound);
    }


}
