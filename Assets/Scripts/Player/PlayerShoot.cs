using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;

    public Transform firePos;
    public float shootDelay = 0.1f;
    public float shootTimer;
    //public vector
    // Start is called before the first frame update
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDelay) 
        {
            shootTimer = 0;
            Instantiate(projectile, firePos.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
}
