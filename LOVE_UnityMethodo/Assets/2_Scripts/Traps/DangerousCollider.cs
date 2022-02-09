using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousCollider : MonoBehaviour
{
    [SerializeField] bool isProjectile = false;

    SpriteRenderer rend;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if(rend.enabled)
        {
            if (!isProjectile)//manage none projectile collision
            {
                if (collision.tag == "Checkpoint")
                {
                    GameManager.Instance.player.currentCheckpoint.DestroyCheckPoint();
                }
                else if(collision.tag == "Player")
                {
                    GameManager.Instance.player.Respawn();
                }
            }
            else //manage projectile collision
            {
                if (collision.tag == "Checkpoint")
                {
                    GameManager.Instance.player.currentCheckpoint.DestroyCheckPoint();
                    rend.enabled = false;
                }
                else if (collision.tag == "Player")
                {
                    GameManager.Instance.player.Respawn();
                    rend.enabled = false;
                }
                else if (collision.tag == "Terrain")
                {
                    rend.enabled = false;
                }

            }
            
            
        }
    }

}
