using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShoot : MonoBehaviour
{
    
    public static EnemyProjectile enemyProjectile;
    public float shotDelay = 1;
    public float itemShotDelay = 0.1f;
    public float shotTimer;
    private float disableTimer = 0f;
    public int numberOfBullets = 30;
    private float bulletSpeed = 10f;
    GameObject playerPos;
    // Update is called once per frame
   
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        if (playerPos ==null)
        {
            return;
        }
        shotTimer += Time.deltaTime;
        if (shotTimer > shotDelay)
        {
            shotTimer = 0;
            if (gameObject.name != "MON_102(Clone)")
            { 
                EliteShot(); 
            }
            else
            {
                NormalShot();
            }

        }
        disableTimer += Time.deltaTime;
        if (disableTimer >= 3f)
        {
            
            //DisableOneBullet();
            disableTimer = 0f;
        }

    }

    
    private void DisableOneBullet()
    {
        foreach (GameObject bullet in ObjectManager.instance.enemyAttack)
        {
            if (bullet.activeInHierarchy)
            {
                bullet.SetActive(false);
                break; // 하나만 비활성화하고 나가기
            }
        }
    }
    private void NormalShot()
    {
        //Debug.Log("dd");
        GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
        //Debug.Log("bb");
        if (enemyProjectile != null)
        {
            var enemyPos = transform.position;
            enemyProjectile.transform.position = enemyPos;
            enemyProjectile.SetActive(true);
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = playerPos.transform.position - enemyPos;
            //Debug.Log("cc");
            //enemyProjectile.transform.position = transform.position+new Vector3(0,-1,0);
        }
    }
    private void EliteShot()
    {
        int shootCount = 0;
        var enemyPos = transform.position;
        var rotateAngle = 15f;
        var firstRot = -15f;
        while (shootCount < 3)
        {
            GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.transform.position = enemyPos;
            enemyProjectile.SetActive(true);
            var rotation = Quaternion.Euler(new Vector3(0, 0, firstRot));
            if (enemyProjectile != null)
            {
                if (shootCount < 1)
                {
                    var direction = rotation * Vector2.down;
                    rb.AddForce(direction* bulletSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    rotation = Quaternion.Euler(new Vector3(0, 15f, (firstRot+rotateAngle*shootCount)));
                    var direction = rotation * Vector2.down;
                    rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
                }


                shootCount++;
               
            }
        }

    }
}
