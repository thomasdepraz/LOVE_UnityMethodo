using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{

    [SerializeField] private float divisionFactor;
    private float originSpeed;
    private float originJumpForce;
    private float originGravity;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float factor = 1 / divisionFactor;
            originSpeed = GameManager.Instance.player.speed;
            originJumpForce = GameManager.Instance.player.jumpForce;
            originGravity = Setting.gravityForce;

            GameManager.Instance.player.speed *= factor;
            GameManager.Instance.player.jumpForce *= factor;
            Setting.gravityForce *= factor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.player.speed *= divisionFactor;
            GameManager.Instance.player.jumpForce *= divisionFactor;
            Setting.gravityForce *= divisionFactor;
        }
    }
}
