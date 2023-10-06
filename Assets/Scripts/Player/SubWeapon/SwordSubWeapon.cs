using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSubWeapon : MonoBehaviour
{
    public GameObject projectile;
    private float shotTimer;
    public float shootDelay = 0.5f;
    public Transform FirePos;
   // public Transform FireRightPos;
    public void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer > 1f)
        {
            Shoot();
            shotTimer = 0f;
        }
    }
    public void Shoot()
    {
        Instantiate(projectile, FirePos.position, Quaternion.identity);

    }

}
