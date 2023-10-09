using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D enemyProjectile;
    private float addTime;
    private void Awake()
    {
        enemyProjectile = GetComponent<Rigidbody2D>();
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
            //Debug.Log(addTime);
            gameObject.SetActive(false);
        }
    }
}
