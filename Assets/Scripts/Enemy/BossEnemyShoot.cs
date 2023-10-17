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
    public float shotTimer = 2f;
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
        StartCoroutine(BossAllShot());

    }
    private IEnumerator BossAllShot()
    {
        ShotReset();
        BossShot2();
        //yield return new WaitForSeconds(shotTimer);
        BossShot1();
        ShotReset();
        yield return new WaitForSeconds(3f);

    }
    private void ShotReset()
    {
        shotCount = 0;
    }
    private void BossShot2()
    {
        while (shotCount < 2)
        {
            GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
            enemyProjectile.SetActive(true);
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.transform.position = firePos1.position;
            if (enemyProjectile != null)
            {
                if(shotCount<1)
                    rb.transform.position = firePos2.position;
                else
                    rb.transform.position = firePos3.position;
                rb.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);
            }
            shotCount++;
        }
    }
    private void BossShot1()
    {
        while(shotCount <20)
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
