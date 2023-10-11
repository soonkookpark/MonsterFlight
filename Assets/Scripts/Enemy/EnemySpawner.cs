using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //public List<EnemyMovement> spawnMonster;
    //public GameObject[] enemies;
    public Enemy bossEnemy;
    public Enemy eliteEnemy;
    public Enemy ItemEnemy;
    public Enemy normalEnemy;
    public List<CinemachineSmoothPath> smoothPath;
    private float checkItemMonster; //아이템 주는 몬스터 체크
    
    private float numberOfSpawn;//스폰할 수
    private float spawnNumber;//스폰 한 수
    private float TimeCheck;//현재 흐른 시간
    private int randNum = 3;
    public bool canSpawn = false;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {

        if(GameManager.Instance.IsGameOver)
        {
            Destroy(gameObject,30f);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        {
            //while (!GameManager.Instance.IsGameOver)
            //{
            //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //    if (enemies.Length < 15)
            //    {
            //        if (Random.Range(0, 2) == 0)
            //        {
            //            Instantiate();
            //        }
            //        else
            //        {
            //            Instantiate(smoothPath[1]);
            //        }
            //    }
            //    bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
            //    if (bossEnemy == null)
            //    {
            //        Instantiate(smoothPath[2]);
            //    }
            //    yield return new WaitForSeconds(5f);
            //}
        }
        while (!GameManager.Instance.IsGameOver && !canSpawn)
        {
            switch(randNum)
            {
                case 0:
                    SpawnItemMonster(randNum);
                    for (int i = 0; i < 2; i++)
                    {
                        yield return new WaitForSeconds(1f);
                        SpawnNormalMonster(randNum);
                    }
                    randNum = Random.Range(1, 7);
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnNormalMonster(randNum);
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnNormalMonster(randNum);
                        SpawnNormalMonster(randNum+1);
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        SpawnNormalMonster(randNum+1);
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }

        }
    }
    private void SpawnNormalMonster(int num)
    {
        var enemy = Instantiate(normalEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        enemy.path = smoothPath[num];
        
    }
    private void SpawnItemMonster(int num)
    {
        var enemy = Instantiate(ItemEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        enemy.path = smoothPath[num];
    }

    //private IEnumerator SpawnEnemies()
    //{
        
    //    switch(randNum)
    //    {
    //        case 0:
    //            SpawnItemPath();
    //            break;
    //    }

    //    yield return new WaitForSeconds(3f);
    //}

    //itemenemy 체크하는곳

}
