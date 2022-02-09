using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForceFactor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!GameManager.Instance.player.isInHelicopterMode)   
                GameManager.Instance.player.VerticalMovement(true, jumpForceFactor);
        }
    }
}
