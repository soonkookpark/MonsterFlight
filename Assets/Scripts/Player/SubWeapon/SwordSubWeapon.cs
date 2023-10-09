using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class SwordSubWeapon : MonoBehaviour
{

    public GameObject projectile;
    private float shotTimer;
    public float shootDelay = 0.5f;
    public Transform FirePos;
    public GameObject[] aibullet;
    // public Transform FireRightPos;
    private ChargeShot chargeShot;
    private bool shouldShoot = true; // Ãß°¡

    public void Start()
    {
        
    }
    public void Update()
    {
        if(chargeShot==null)
            chargeShot = GetComponent<ChargeShot>();
        shotTimer += Time.deltaTime;
        if (shotTimer > 1f&& shouldShoot)
        {
            aibullet = GameObject.FindGameObjectsWithTag("Aibullet");
            if(aibullet.Length<6)
                Shoot();

            shotTimer = 0f;
        }
    }

    public void Shoot()
    {
        if(chargeShot==null)
            Instantiate(projectile, FirePos.position, Quaternion.identity);
    }

}
