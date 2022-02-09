using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //set helicopter mode
            if (!GameManager.Instance.player.isInHelicopterMode)
                GameManager.Instance.player.isInHelicopterMode = true;
        }
    }
}
