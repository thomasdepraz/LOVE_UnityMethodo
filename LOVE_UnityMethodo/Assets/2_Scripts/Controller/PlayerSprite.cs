using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private bool animState = false;
    private float lastSec;

    public Sprite[] sprites;

    public SpriteRenderer sr;
    void Start()
    {
        lastSec = Time.time;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time + 1f > lastSec)
        {
            lastSec = Time.time;

            if (animState)
            {
                animState = false;
                sr.sprite = sprites[0];
            }
            else
            {
                animState = true;
                sr.sprite = sprites[1];
            }
        }
    }
}
