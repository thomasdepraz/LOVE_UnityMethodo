using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotativeSaw : MonoBehaviour
{
    public Transform[] positions = new Transform[2];
    public float rotateSpeed;
    public float movementSpeed;

    [Range(-1,1)]
    public int rotationDirection;
    int currentIndex = -1;

    public void OnBecameVisible()
    {
        if (positions[0] != null)
        {
            if (transform.position == positions[0].position) currentIndex = 0;
            Move(GetToIndex());
        }
        Rotate();
    }

    public void OnBecameInvisible()
    {
        LeanTween.cancel(gameObject);  
    }

    public void Move(int toIndex)
    {
        currentIndex = toIndex;
        LeanTween.move(gameObject, positions[toIndex].position, movementSpeed).setOnComplete(()=>Move(GetToIndex()));
    }

    public void Rotate()
    {
        if (gameObject.transform.rotation.eulerAngles.z == 360) gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        LeanTween.rotate(gameObject, new Vector3(0, 0, gameObject.transform.rotation.eulerAngles.z + 180 * rotationDirection), rotateSpeed).setOnComplete(Rotate);
    }

    int GetToIndex()
    {
        switch (currentIndex)
        {
            case -1: return 0;

            case 0: return 1;

            case 1: return 0;

            default: return 0;
        }
    }
   

}
