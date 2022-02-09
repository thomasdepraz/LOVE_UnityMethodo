using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerFX : MonoBehaviour
{

    private PlayerController pc;
    private ParticleSystem ps;

    //private bool isPlayingPS = false;
    //public bool fxDuration = 
    
    void Start()
    {
        pc = GetComponent<PlayerController>();
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayerDies()
    {
        ps.startColor = Color.yellow;
        ps.Play();
        //Destroy(gameObject, ps.main.duration);
    }

    public void PlayerSpaws()
    {
        ps.Play();
    }

    public void DisableAllFX()
    {
        ps.startColor = Color.red;
        ps.Stop();
    }


}
