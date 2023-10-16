using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemEnemyShoot : MonoBehaviour
{
    public Transform firePos;
    public EnemyProjectile enemyProjectile;
    public float itemShotDelay = 0.1f;
    public float shotTimer;
    public int numberOfBullets = 30;
    private float currentAngle = 0f;
    private float angleIncrement;
    private float disableTimer = 0f;
    GameObject playerPos;
    public int shotCount;
    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        if (playerPos == null)
        {
            return;
        }

        //���� ��� 30���� �ѱ�� 5�� �� �� �ٽ� ��� ��.
        shotTimer += Time.deltaTime;
        if ((shotTimer > itemShotDelay) && shotCount <= 30)
        {
            shotTimer = 0;
            ItemShot();
        }
        else if (shotTimer > 5)
        {
            shotCount = 0;
        }


        //disableTimer += Time.deltaTime;
        //if (disableTimer >= 2f)
        //{
        //    DisableOneBullet();
        //    disableTimer = 0f;
        //}

    }

    private void ItemShot()
    {

        GameObject enemyProjectile = ObjectManager.instance.GetEnemyBullet();
        //Debug.Log("bb");
        angleIncrement = 360f / numberOfBullets;
        if (angleIncrement > 359)
        {
            return;
        }
        if (enemyProjectile != null)
        {
            enemyProjectile.SetActive(true);
            
            enemyProjectile.transform.position = firePos.position;

            // �߻� ���� ���� (�������� ��ġ)
            Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), -Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * 10;

            // ���� �߻��� �Ѿ��� ������ ���� (����)
            currentAngle += angleIncrement;
            shotCount++;
            //Debug.Log(rb.velocity);
            //Debug.Log(currentAngle);

        }
    }

    private void DisableOneBullet()
    {
        foreach (GameObject bullet in ObjectManager.instance.enemyAttack)
        {
            if (bullet.activeInHierarchy)
            { 
                bullet.SetActive(false);
                break; // �ϳ��� ��Ȱ��ȭ�ϰ� ������
            }
        }
    }
}
