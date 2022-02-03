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
            else if(isProjectile)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
