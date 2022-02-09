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
        Debug.Log("player dies");
        ps.startColor = Color.red;
        ps.Emit(20);
        //Destroy(gameObject, ps.main.duration);
    }

    public void PlayerSetNewSpawn()
    {
        Debug.Log("new spawn");
        ps.startColor = Color.yellow;
        ps.Emit(20);
    }

    public void PlayerSpawns()
    {
        Debug.Log("player spawn");
        ps.startColor = Color.white;
        ps.Emit(20);
    }

    public void DisableAllFX()
    {
        ps.Stop();
    }


}
