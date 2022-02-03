using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousCollider : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            GameManager.Instance.player.currentCheckpoint.DestroyCheckPoint();
            Debug.Log("hit checkpoint");
        }
        else if(collision.tag == "Player")
        {
            GameManager.Instance.player.Respawn();
            Debug.Log("hit player");
        }
    }

}
