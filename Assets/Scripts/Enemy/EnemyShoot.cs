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
            NormalShot();

        }
        disableTimer += Time.deltaTime;
        if (disableTimer >= 3f)
        {
            
            DisableOneBullet();
            disableTimer = 0f;
        }
    }

    //private void Shot()
    //{
    //    switch (gameObject.tag)
    //    {
    //        case "Enemy":
    //            NormalShot();
    //            break;
    //        case "ItemEnemy":
    //            ItemShot();
    //            break;
    //        case "Boss":
    //            break;
    //    }
    //}
    //private IEnumerator NormalShot()
    //{
    //    //Debug.Log("dd");
    //    GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
    //    //Debug.Log("bb");

    //        if (enemyProjectile != null)
    //        {
    //            var playerPos = GameObject.FindGameObjectWithTag("Player");
    //            var enemyPos = GameObject.FindGameObjectWithTag("Enemy");
    //            enemyProjectile.transform.position = enemyPos.transform.position;
    //            enemyProjectile.SetActive(true);
    //            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
    //            rb.velocity = playerPos.transform.position - enemyPos.transform.position;
    //            Debug.Log("cc");
    //            yield return new WaitForSeconds(5f);
    //            enemyProjectile.SetActive(false);
    //            //enemyProjectile.transform.position = transform.position+new Vector3(0,-1,0);
    //        }        
    //}
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

    //private void ItemShot()
    //{
    //    //if(currentAngle)
    //    GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
    //    //Debug.Log("bb");
    //    angleIncrement = 360f / numberOfBullets;
    //    if(angleIncrement>359)
    //    {
    //        return;
    //    }
    //    if (enemyProjectile != null)
    //    {
    //        enemyProjectile.SetActive(true);
    //        var enemyPos = transform.position;
    //        enemyProjectile.transform.position = enemyPos;

    //        // 발사 각도 설정 (원형으로 배치)
    //        Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

    //        var rb = enemyProjectile.GetComponent<Rigidbody2D>();
    //        rb.velocity = direction * 10;

    //        // 다음 발사할 총알의 각도를 설정 (누적)
    //        currentAngle += angleIncrement;

    //        Debug.Log(rb.velocity);
    //        Debug.Log(currentAngle);
    //    }
    //}

}
