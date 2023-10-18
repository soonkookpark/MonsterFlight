using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossEnemyShoot : MonoBehaviour
{
    public EnemyProjectile enemyProjectile;
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public float shotTimer1 = 1.4f;
    public float shotTimer2 = 3f;
    GameObject playerPos;
    public int numberOfBullets = 20;
    private float currentAngle = 168;
    private float angleIncrement;
    private int shotCount1;
    private int shotCount2;
    private float disableTimer1 = 0f;
    private float disableTimer2 = 0f;
    private bool shotDegreeChange = true;

    private void Update()
    {
        disableTimer1 += Time.deltaTime;
        disableTimer2 += Time.deltaTime;
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
        BossAllShot();

    }
    private void BossAllShot()
    {
        if (shotTimer1 < disableTimer1)
        {
            Shot1Reset();
            BossShot2();
            disableTimer1 = 0;
        }
        if (shotTimer2 < disableTimer2)
        {
            Shot2Reset();
            BossShot1();
            disableTimer2 = 0;
        }
    }
    private void Shot1Reset()
    {
        shotCount1 = 0;
    }
    private void Shot2Reset()
    {
        shotCount2 = 0;
    }
    private void BossShot2()
    {
        while (shotCount2 < 2)
        {
            GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
            enemyProjectile.SetActive(true);
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.transform.position = firePos1.position;
            if (enemyProjectile != null)
            {
                if(shotCount2<1)
                    rb.transform.position = firePos2.position;
                else
                    rb.transform.position = firePos3.position;
                rb.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);
            }
            shotCount2++;
        }
    }
    private void BossShot1()
    {
        while(shotCount1 <20)
        {
            
            GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
            //enemyProjectile.transform.localScale *= 1.5f;
            //enemyProjectile.GetComponent<BoxCollider2D>().size *= 1.5f;
            
            //Debug.Log("bb");
            angleIncrement = 360f / numberOfBullets;
            if (enemyProjectile != null)
            {
                enemyProjectile.SetActive(true);

                //var enemyPos = transform.position;
                enemyProjectile.transform.position = firePos1.position;

                // 발사 각도 설정 (원형으로 배치)
                Vector2 direction = new(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                var rb = enemyProjectile.GetComponent<Rigidbody2D>();
                rb.velocity = direction * 10;

                // 다음 발사할 총알의 각도를 설정 (누적)
                currentAngle += angleIncrement;
                shotCount1++;
                //Debug.Log(rb.velocity);
                //Debug.Log(currentAngle);
            }
        }
    }

}
