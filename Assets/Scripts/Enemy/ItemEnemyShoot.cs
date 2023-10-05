using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemyShoot : MonoBehaviour
{
    public float itemShotDelay = 0.1f;
    public float shotTimer;
    public int numberOfBullets = 30;
    private float currentAngle = 0f;
    private float angleIncrement;
    private float disableTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((shotTimer > itemShotDelay) && gameObject.CompareTag("ItemEnemy"))
        {
            shotTimer = 0;
            ItemShot();
        }
        disableTimer += Time.deltaTime;
        if (disableTimer >= 2f)
        {
            DisableOneBullet();
            disableTimer = 0f;
        }
    }

    private void ItemShot()
    {
        //if(currentAngle)
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
            var enemyPos = transform.position;
            enemyProjectile.transform.position = enemyPos;

            // �߻� ���� ���� (�������� ��ġ)
            Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            var rb = enemyProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * 10;

            // ���� �߻��� �Ѿ��� ������ ���� (����)
            currentAngle += angleIncrement;

            Debug.Log(rb.velocity);
            Debug.Log(currentAngle);
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
