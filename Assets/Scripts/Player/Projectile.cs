using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    PlayerShoot p1;
    
    Rigidbody2D projectileRigid;
    public float speed = 30f;
    public int damage = 1;

    private void Awake()
    {
        projectileRigid = GetComponent<Rigidbody2D>();
        //p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShoot>();
        //Debug.Log("1¹ø"+p1);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        //Debug.Log("2¹ø" + p1);
        ProjectileMove();
        Destroy(gameObject,3f);
    }

    private void ProjectileMove()
    {
        projectileRigid.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")|| collision.CompareTag("ItemEnemy")|| collision.CompareTag("BossEnemy"))
        {
            //Debug.Log("3¹ø" + p1);
            p1.CountUp();
            Destroy(gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if(collision.CompareTag("DieZone")||collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
