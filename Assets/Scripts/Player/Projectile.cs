using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D projectileRigid;
    public float speed = 30f;
    public int damage = 1;

    private void Awake()
    {
        projectileRigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        ProjectileMove();
        Destroy(gameObject,3f);
    }

    private void ProjectileMove()
    {
        projectileRigid.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
