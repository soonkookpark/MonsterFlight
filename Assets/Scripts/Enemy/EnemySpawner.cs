using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyMovement> spawnMonster;
    public GameObject[] enemies;
    public GameObject bossEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            Destroy(gameObject,30f);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while(!GameManager.Instance.IsGameOver)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length<15)
            {
                if(Random.Range(0,2)==0)
                {
                    Instantiate(spawnMonster[0]);
                }
                else
                {
                    Instantiate(spawnMonster[1]);
                }
            }
            bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
            if (bossEnemy == null)
            {
                Instantiate(spawnMonster[2]);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
