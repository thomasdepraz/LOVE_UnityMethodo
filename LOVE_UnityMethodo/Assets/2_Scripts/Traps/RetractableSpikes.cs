using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikes : MonoBehaviour
{
    internal enum SpikeState {IN, OUT}
    public float inDuration;
    public float outDuration;
    SpikeState currentState = SpikeState.OUT;
    Coroutine spikeRoutine;

    public GameObject visuals;


    private void OnBecameVisible()
    {
        spikeRoutine = StartCoroutine(SpikeRoutine());
    }


    private void OnBecameInvisible()
    {
        if (spikeRoutine != null) StopCoroutine(spikeRoutine);
    }

    private IEnumerator SpikeRoutine()
    {
        bool tween = true;
        switch (currentState)
        {
            case SpikeState.IN:
                Tween(true, ()=> tween = false);
                while(tween) { yield return new WaitForEndOfFrame(); }

                yield return new WaitForSeconds(outDuration);
                break;

            case SpikeState.OUT:
                Tween(false, () => tween = false);
                while (tween) { yield return new WaitForEndOfFrame(); }

                yield return new WaitForSeconds(inDuration);
                break;

            default:
                Debug.LogError("INCORRECT STATE");
                break;
        }
        spikeRoutine = StartCoroutine(SpikeRoutine());
    }

    public void Tween(bool isIn, Action onComplete)
    {
        if(isIn)//Deploy
        {
            visuals.SetActive(true);
            currentState = SpikeState.OUT;
            onComplete?.Invoke();
        }
        else//Retract
        {
            visuals.SetActive(false);
            currentState = SpikeState.IN;
            onComplete?.Invoke();
        }
    }
}
