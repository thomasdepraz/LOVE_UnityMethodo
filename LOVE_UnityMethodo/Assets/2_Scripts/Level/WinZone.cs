using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public bool autoWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!autoWin)
                GameManager.Instance.LoadLevel(GameManager.Instance.NextLevel());
            else
                GameManager.Instance.LoadLevel();
        }
    }
}
