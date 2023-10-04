using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public EnemyProjectile enemyProjectile;
    private ObjectManager bulletPool;
    public float shotDelay = 1;
    public float shotTimer;
    public Transform tf;
    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer > shotDelay)
        {
            shotTimer = 0;
            Shot();
        }
    }

    private void Shot()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                NormalShot();
                break;
            case "ItemEnemy":
                ItemShot();
                break;
            case "Boss":
                break;
        }
    }
    private void NormalShot()
    {
        //Debug.Log("dd");
        GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
        //Debug.Log("bb");
        if (enemyProjectile != null)
        {
            var playerPos = GameObject.FindGameObjectWithTag("Player");
            var enemyPos = GameObject.FindGameObjectWithTag("Enemy");
            enemyProjectile.transform.position = enemyPos.transform.position;
            enemyProjectile.SetActive(true);
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = playerPos.transform.position-enemyPos.transform.position;
            Debug.Log("cc");
            //enemyProjectile.transform.position = transform.position+new Vector3(0,-1,0);

        }
    }
    private void ItemShot()
    {
        GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
        //Debug.Log("bb");
        if (enemyProjectile != null)
        {
            var playerPos = GameObject.FindGameObjectWithTag("Player");
            var enemyPos = GameObject.FindGameObjectWithTag("Enemy");
            enemyProjectile.transform.position = enemyPos.transform.position;
            enemyProjectile.SetActive(true);
            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = playerPos.transform.position - enemyPos.transform.position;
            Debug.Log("cc");
            //enemyProjectile.transform.position = transform.position+new Vector3(0,-1,0);

        }
    }

}
