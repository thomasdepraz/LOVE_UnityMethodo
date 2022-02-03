using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public Image fillbar;
    public float fillDuration;
    public float fillCooldown;
    public SpriteRenderer laserRenderer;


    public void OnBecameVisible()
    {
        laserRenderer.enabled = false;
        Fill();
    }

    public void OnBecameInvisible()
    {
        LeanTween.cancel(gameObject);
    }

    public void Fill()
    {
        LeanTween.value(gameObject, 0, 1, fillDuration).setOnUpdate((value)=> fillbar.fillAmount = value).setOnComplete(() => 
        {
            SwitchLaser();
            Wait();
        });
    }

    public void Wait()
    {
        LeanTween.value(gameObject, 1, 0, fillCooldown).setOnUpdate((value) => fillbar.fillAmount = value).setOnComplete(()=> 
        {
            SwitchLaser();
            Fill();
        });

    }

    public void SwitchLaser()
    {
        if (laserRenderer.enabled) laserRenderer.enabled = false;
        else laserRenderer.enabled = true;
    }


}
