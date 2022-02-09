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
        audioSource.volume = GameManager.Instance.volume / 100;
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayerDies()
    {
        audioSource.volume = GameManager.Instance.volume / 100;
        audioSource.PlayOneShot(deathSound);
    }


}
