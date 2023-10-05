//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;

//public class AiProjectile : MonoBehaviour
//{
//    PlayerShoot p1;

//    Rigidbody2D AiProjectileRigid;
//    public float speed = 1f;
//    public int damage = 1;
//    //경로 재탐색 시간
//    public float findTime = 0.1f;
//    private float addTime;
//    //탄환의 현재 위치



//    private void Awake()
//    {
//        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
//        AiProjectileRigid = GetComponent<Rigidbody2D>();
//    }

//    private void Start()
//    {
//        Destroy(gameObject, 3f);
//    }
//    private void Update()
//    {
//        //addTime += Time.deltaTime;
//        //if (addTime > findTime)
//            AiProjectileMove();
//    }
//    private GameObject FindTarget()
//    {
//        var result = FindAllEnemyTag("Enemy", "ItemEnemy","BossEnemy");
//        GameObject closestTarget = null;
//        float cDistance = Mathf.Infinity;
//        foreach (var target in result)
//        {
//            float newDistance = Vector3.Distance(transform.position, target.transform.position);

//            if(newDistance<cDistance)
//            {
//                cDistance = newDistance;
//                closestTarget = target;
//            }
//        }

//        return closestTarget;
//    }

//    private List<GameObject> FindAllEnemyTag(params string[] tagName)
//    {
//        List<GameObject> enemyList = new List<GameObject>();

//        foreach (var tag in tagName)
//        {
//            enemyList.AddRange(GameObject.FindGameObjectsWithTag(tag));
//        }

//        return enemyList;
//    }

//    private void AiProjectileMove()
//    {
//        GameObject target = FindTarget();
//        Vector2 moveDirection = (target.transform.position - transform.position).normalized;

//        AiProjectileRigid.MovePosition(moveDirection * speed * Time.deltaTime);

//        //target.transform.Translate(moveDirection * speed * Time.deltaTime);
//    }
//    public void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Enemy") || collision.CompareTag("ItemEnemy") || collision.CompareTag("BossEnemy"))
//        {

//            p1.CountUp();
//            Destroy(gameObject);
//            collision.GetComponent<Enemy>().TakeDamage(damage);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiProjectile : MonoBehaviour
{
    PlayerShoot p1;

    Rigidbody2D projectileRigid;
    public float speed = 0.5f;
    public int damage = 1;

    private GameObject closestTarget; // 가장 가까운 타겟을 저장할 변수

    private void Awake()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        projectileRigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //FindClosestTarget(); // 시작할 때 가장 가까운 타겟을 찾습니다.
        //ProjectileMove();
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        FindClosestTarget(); // 업데이트마다 가장 가까운 타겟을 찾습니다.
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