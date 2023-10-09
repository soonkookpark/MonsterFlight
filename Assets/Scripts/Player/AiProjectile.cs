using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiProjectile : MonoBehaviour
{
    PlayerShoot p1;

    Rigidbody2D projectileRigid;
    public float speed = 0.5f;
    public int damage = 1;

    private GameObject closestTarget; 

    private void Awake()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        projectileRigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        FindClosestTarget(); 
        ProjectileMove();
    }

    private void ProjectileMove()
    {
        if (closestTarget != null)
        {
            Vector3 moveDirection = (closestTarget.transform.position - transform.position).normalized;
            projectileRigid.velocity = moveDirection * speed;
        }
    }

    private void FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] itemTargets = GameObject.FindGameObjectsWithTag("ItemEnemy");
        GameObject[] bossTargets = GameObject.FindGameObjectsWithTag("BossEnemy");

        List<GameObject> allTargets = new List<GameObject>();
        allTargets.AddRange(targets);
        allTargets.AddRange(itemTargets);
        allTargets.AddRange(bossTargets);

        closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in allTargets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("ItemEnemy") || collision.CompareTag("BossEnemy"))
        {
            p1.CountUp();
            Destroy(gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}