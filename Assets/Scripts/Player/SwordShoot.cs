using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShoot : PlayerShoot
{
    public Transform[] firePos; // 크기가 4인 배열
    public GameObject projectile;

    public float shootDelay = 0.3f;
    public float shootTimer;
    public float subWeaponCount = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    protected void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer>shootDelay)
        {
            Shoot();
            shootTimer = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Ult();
        }
    }
    void Shoot()
    {
        for (int i = 0; i < firePos.Length; i++)
        {
            Instantiate(projectile, firePos[i].position, Quaternion.identity);
        }
    }
    public override void CountUp()
    {

        subWeaponCount++;
        //Debug.Log(subWeaponCount);
    }
    void Ult()
    {
        Debug.Log("여기 다녀감");
        
        foreach (GameObject bullet in ObjectManager.instance.enemyAttack)
        {
            if (bullet.activeInHierarchy)
            {
                bullet.SetActive(false);
            }
        }
    }
}
