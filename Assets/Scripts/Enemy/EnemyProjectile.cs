using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D enemyProjectile;
    BoxCollider2D enemyProjectileBoxCollider;
    private float addTime;

    private void Awake()
    {
        enemyProjectile = GetComponent<Rigidbody2D>();
        enemyProjectileBoxCollider = GetComponent<BoxCollider2D>();
        enemyProjectile.transform.localScale = new Vector2(0.3f, 0.3f);
    }

    private void Update()
    {
        addTime += Time.deltaTime;
        if(enemyProjectile != null && addTime>5.0f)
        {
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DieZone")|| collision.CompareTag("DeadZone") || collision.CompareTag("Player"))
        {
            enemyProjectile.transform.localScale = new Vector2(0.3f, 0.3f);
            enemyProjectileBoxCollider.size = new Vector2(1f,1f);
            //Debug.Log(addTime);
            gameObject.SetActive(false);
        }
    }
}
