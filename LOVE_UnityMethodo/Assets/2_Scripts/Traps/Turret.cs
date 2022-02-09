using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angleFromCenter;
    public float rotateSpeed;
    public float cooldown;
    public List<Projectile> projectiles = new List<Projectile>();
    private Vector3 originEulerAngle;
    private Coroutine cooldownRoutine;

    private void OnBecameVisible()
    {
        originEulerAngle = transform.rotation.eulerAngles;
        cooldownRoutine = StartCoroutine(Cooldown());
        Pan();
    }


    private void OnBecameInvisible()
    {
        transform.rotation = Quaternion.Euler(originEulerAngle) ;
        LeanTween.cancel(gameObject);
        StopCoroutine(cooldownRoutine);
    }

    public void Pan()
    {
        LeanTween.rotate(gameObject, GetPanTarget(), rotateSpeed).setOnComplete(Pan);
    }

    public Vector3 GetPanTarget()
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        if (currentEuler == originEulerAngle) 
            return new Vector3(0, 0, originEulerAngle.z + angleFromCenter);

        return originEulerAngle;
    }

    public void Shoot()
    {
        Projectile projectile = GetProjectile();
        if(projectile!=null)
        {
            projectile.self.position = projectile.originPosition;
            projectile.direction = transform.up;
            projectile.rend.enabled = true;
        }
    }

    Projectile GetProjectile()
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            if (!projectiles[i].rend.enabled) return projectiles[i];
        }
        return null;
    }

    public IEnumerator Cooldown()
    {
        Shoot();
        yield return new WaitForSeconds(cooldown);
        cooldownRoutine =  StartCoroutine(Cooldown());
    }

}
