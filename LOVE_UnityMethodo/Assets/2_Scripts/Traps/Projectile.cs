using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Transform self;
    public SpriteRenderer rend;
    [SerializeField] private float speed;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 originPosition;

    bool moving;

    private void OnBecameVisible()
    {
        moving = true;
        originPosition = self.position;
    }


    private void OnBecameInvisible()
    {
        moving = false;
        self.position = originPosition;
    }

    public void Update()
    {
        if(moving)
        {
            self.position += direction * speed * Time.deltaTime;
        }
    }
}
