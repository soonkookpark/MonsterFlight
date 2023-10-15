using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    //public List<EnemyMovement> spawnMonster;
    //public GameObject[] enemies;
    public List<Enemy> enemy;
    private Enemy reallySpawnEnemy;
    //public Enemy eliteEnemy;
    //public Enemy ItemEnemy;
    //public Enemy normalEnemy;
    public List<CinemachineSmoothPath> smoothPath;
    int currentRoot = 1;
    MonsterSpawnTable monsterSpawnTable;
    private Dictionary<int, MonsterSpawnTable.Data> spawnInfo = new Dictionary<int, MonsterSpawnTable.Data>();
    private int randNum = 0;
    public bool canSpawn = false;
    int stageNum=1;
    int courseNum = 1;
    //int patternType=1;
    float startPos;
    float spawnTime;
    //float mag;
    //���� �ð�
    float nowAddTime;
    string monsterID;
    int amount;
    private void Awake()
    {

        // MonsterSpawnTable Ŭ������ �ν��Ͻ� ����
        monsterSpawnTable = new MonsterSpawnTable();
        // ������ �ε�
        monsterSpawnTable.Load();
        // �����͸� spawnInfo ��ųʸ��� ����
        LoadData();


        //spawnInfo.Add()
    }
    void Start()
    {
        LoadData();

        StartCoroutine(SpawnEnemies());
    }
    void LoadData()
    {
        var allSpawnData = monsterSpawnTable.GetAllSpawnData();

        foreach (var data in allSpawnData)
        {
            spawnInfo[data.Root] = data;
        }
        
    }
    void Update()
    {
        //while (!GameManager.Instance.IsGameOver && !canSpawn)
        
        //�´� ����Ÿ���� ã��
        //�ش�����Ÿ���� ù ��Ʈ�� �����´�.
        //�ð��� �����ϰ�
        //���Ͱ� �� ������, ��Ʈ�� �ϳ� �ø���
        //if (spawnInfo[currentRoot].PatternType == (patternType % 2))
        //{
        //    currentRoot++;
        //    if (spawnInfo.Count <= currentRoot)
        //    {
        //        currentRoot = 1;
        //    }
        //    if (spawnInfo[currentRoot].PatternType == (patternType % 2))
        //    {
        //        var monData = spawnInfo[currentRoot];
        //        if (monData.AppearTime < nowAddTime)

        //            monsterID = monData.Mon_ID; //��ȯ�� ���� ���̵�
        //        spawnTime = monData.AppearTime; //���� �ð�
        //        courseNum = monData.Way;
        //        startPos = monData.StartPoint;
        //        amount = monData.Amount;





        //    }
        //    else
        //    {
        //        patternType++;
        //    }


        //}


        ////Ÿ���� ���� �ο�
        ////patternType = data.Value.PatternType;
        ////spawnTime = data.Value.AppearTime;
        
        //if (stageNum % 2 == patternType && spawnTime >= nowAddTime)
        //{
        //Debug.Log(nowAddTime);
        //}
        if (GameManager.Instance.IsGameOver)
        {
            Destroy(gameObject, 30f);
        }
        
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            nowAddTime += Time.deltaTime;
            //���� ���̺��� �� ��ȸ���� ���
            if (spawnInfo.Count <= currentRoot)
            {
                currentRoot = 1;
            }
            //�´� ����Ÿ���� ã��
            if (spawnInfo[currentRoot].PatternType == (stageNum % 2))
            {
                //�ش�����Ÿ���� ù ��Ʈ�� �����´�.
                var monData = spawnInfo[currentRoot];
                //��ȯ�� �ð��� �ȴٸ� �ɷ�ġ�� �Ҵ���.
                if (monData.AppearTime < nowAddTime)
                {
                    spawnTime = nowAddTime;
                    monsterID = monData.Mon_ID; //��ȯ�� ���� ���̵�
                                                //spawnTime = monData.AppearTime; //���� �ð�
                    courseNum = monData.Way;
                    startPos = monData.StartPoint;
                    amount = monData.Amount;
                    SpawnMonster(courseNum, startPos, monsterID, amount);
                    currentRoot++;
                    yield return null;
                }
                //�ð��� �����ϰ�
                //���Ͱ� �� ������, ��Ʈ�� �ϳ� �ø���
            }
            else
            {
                currentRoot++;
            }
            //���� ����Ÿ���� �ٸ���� ��Ʈ�� ���ؼ� �´� ����Ÿ���� ã�´�.

            //currentRoot++;
            CheckTable();
            yield return null;
            //�ٸ��� ��ȣ�� ���ϰ�
        }
    }
    private void SpawnMonster(int way, float xPos, string ID, int num)
    {

        foreach(var e in enemy)
        {
            if(e.gameObject.name==ID)
            {
                reallySpawnEnemy = e;
                break;
            }
        }
        var realStartPos = new Vector3(smoothPath[way].m_Waypoints[0].position.x + xPos, smoothPath[way].m_Waypoints[0].position.y);
        var spawnEnemy = Instantiate(reallySpawnEnemy, realStartPos, Quaternion.identity);
        //spawnEnemy.GetComponent<Enemy>().InitializePath(smoothPath[way], realStartPos);
        spawnEnemy.path = smoothPath[way];
        spawnEnemy.SetMonsterID(ID);
        //���߿� �����Ϸ� ���̵� ������ �� �ִ� ��� �߰�
    }
    private void CheckTable()
    {
        if (spawnInfo.Count < currentRoot)
            currentRoot = 0;
    }
    private void SpawnNormalMonster(int num)
    {
        //var enemy = Instantiate(normalEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        //enemy.path = smoothPath[num];
        //enemy.SetMonsterID(monsterID);
    }
    private void SpawnItemMonster(int num)
    {
        //var enemy = Instantiate(ItemEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        //enemy.path = smoothPath[num];
       // enemy.SetMonsterID(monsterID);
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

    //itemenemy üũ�ϴ°�

}
