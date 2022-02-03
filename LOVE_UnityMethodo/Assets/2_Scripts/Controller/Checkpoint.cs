using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    public Vector3 position;
    public GameObject container;

    public Checkpoint(Vector3 position, GameObject checkpointVisuals)
    {
        this.position = position;
        container = checkpointVisuals;
        checkpointVisuals.transform.position = position;
        checkpointVisuals.SetActive(true);
    }

    public void DestroyCheckPoint()
    {
        container.SetActive(false);
        GameManager.Instance.player.currentCheckpoint = null;
    }
}
