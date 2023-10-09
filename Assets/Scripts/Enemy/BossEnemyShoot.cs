using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShoot : MonoBehaviour
{
    public EnemyProjectile enemyProjectile;
    public float shotTimer;
    GameObject playerPos;
    public int numberOfBullets = 20;
    private float currentAngle = 168;
    private float angleIncrement;
    private int shotCount;
    private float disableTimer = 0f;
    private bool shotDegreeChange = true;

    private void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        if (playerPos == null)
        {
            return;
        }



        if (shotDegreeChange)
        {
            currentAngle = 173;
            shotDegreeChange = !shotDegreeChange;
        }
        else
        {
            currentAngle = 168;
            shotDegreeChange = !shotDegreeChange;
        }
        BossShot();

        shotTimer += Time.deltaTime;
        if(shotTimer>2f)
        {
            shotCount = 0;
            shotTimer = 0f;
        }

        disableTimer += Time.deltaTime;
        if (disableTimer >= 2f)
        {
            DisableOneBullet();
            disableTimer = 0f;
        }
    }


    private void BossShot()
    {
        while(shotCount <20)
        {
            
            GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
            //Debug.Log("bb");
            angleIncrement = 360f / numberOfBullets;
            if (enemyProjectile != null)
            {
                enemyProjectile.SetActive(true);

                var enemyPos = transform.position;
                enemyProjectile.transform.position = enemyPos;

                // 발사 각도 설정 (원형으로 배치)
                Vector2 direction = new(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                var rb = enemyProjectile.GetComponent<Rigidbody2D>();
                rb.velocity = direction * 10;

                // 다음 발사할 총알의 각도를 설정 (누적)
                currentAngle += angleIncrement;
                shotCount++;
                //Debug.Log(rb.velocity);
                //Debug.Log(currentAngle);
            }
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
}
